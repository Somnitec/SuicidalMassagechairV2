using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeSystem.Functions;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodePlayingLogic
{
    [Title("Debug Info")]
    [ReadOnly, ShowInInspector]
    public bool AudioFinished { get; private set; }

    [ReadOnly, ShowInInspector] public bool FunctionsFinished { get; private set; }
    private float functionDuration = 0f;
    private float functionProgress => Mathf.Clamp(functionDuration, 0, now - invokedTime);
    private float invokedTime = 0f;
    private float now => Time.timeSinceLevelLoad;
    public string FunctionProgress => $"[{functionProgress:F2}/{functionDuration:F2}]";

    public void PlayFunctionsAndAudio(Action onFinished, AudioClip clip, FunctionList funcs, string name,
        NodeFunctionRunner coroutineRunner)
    {
        coroutineRunner.StopAllCoroutines();
        coroutineRunner.StartCoroutine(
            InvokeFunctionsAndPlayAudioCoroutine(
                name,
                clip,
                funcs,
                onFinished,
                coroutineRunner));
    }

    private IEnumerator InvokeFunctionsAndPlayAudioCoroutine(string name,
        AudioClip clip,
        FunctionList funcs,
        Action onFinished,
        MonoBehaviour coroutineRunner)
    {
        FunctionsFinished = false;
        AudioFinished = false;
        invokedTime = now;
        functionDuration = funcs?.Duration ?? 0;

        if (clip == null)
        {
            Debug.LogWarning($"No audioClip on data of {name}");
            AudioFinished = true;
        }
        else
        {
            AudioManager.Instance.PlayClip(
                clip,
                OnAudioFinished,
                SettingsHolder.Instance.Settings.AllowInputFasterInSeconds);
        }

        coroutineRunner.StartCoroutine(ExecuteFunctions(funcs, coroutineRunner));

        while (!AudioFinished)
            yield return null;

        onFinished?.Invoke();
    }

    private void OnAudioFinished()
    {
        AudioFinished = true;
    }

    public IEnumerator InvokeFunctionsCoroutine(FunctionList funcs, Action onFinished, MonoBehaviour coroutineRunner)
    {
        FunctionsFinished = false;

        yield return ExecuteFunctions(funcs, coroutineRunner);

        onFinished?.Invoke();
    }

    private IEnumerator ExecuteFunctions(FunctionList funcs, MonoBehaviour coroutineRunner)
    {
        float timeStarted = Time.timeSinceLevelLoad;

        if (funcs == null)
        {
            FunctionsFinished = true;
            yield break;
        }

        funcs.Sort();

        foreach (var nodeScriptLine in funcs.Functions)
        {
            var timePassed = TimePassed(timeStarted);

            while (timePassed < nodeScriptLine.TimeSec)
            {
                yield return null;
                timePassed = TimePassed(timeStarted);
            }

            timePassed = TimePassed(timeStarted);
            if (SettingsHolder.Instance.Settings.LogDebugMessageInfo)
                Debug.Log($"Node Function: {nodeScriptLine.Function?.GetType().FullName} at {timePassed}");
            nodeScriptLine.Function?.RaiseEvent(coroutineRunner);
        }

        FunctionsFinished = true;
        functionDuration = 0;
    }

    private static float TimePassed(float timeStarted)
    {
        return Time.timeSinceLevelLoad - timeStarted;
    }
}