using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeLogic
{
    [Title("Debug Info")]
    [ReadOnly, ShowInInspector] public bool AudioFinished { get; private set; }
    [ReadOnly, ShowInInspector] public bool FunctionsFinished { get; private set; }

    public IEnumerator InvokeFunctionsAndPlayAudioCoroutine(string Name, AudioClip clip, FunctionList funcs, Action onFinished)
    {
        FunctionsFinished = false;
        AudioFinished = false;

        if (clip == null)
        {
            Debug.LogWarning($"No audioClip on data of {Name}");
            AudioFinished = true;
        }
        else
        {
            AudioManager.Instance.PlayClip(clip, () => AudioFinished = true);
        }

        yield return ExecuteFunctions(funcs);
        while (!AudioFinished || !FunctionsFinished)
            yield return null;

        onFinished?.Invoke();
    }

    public IEnumerator InvokeFunctionsCoroutine(FunctionList funcs, Action onFinished)
    {
        FunctionsFinished = false;

        yield return ExecuteFunctions(funcs);

        onFinished?.Invoke();
    }

    private IEnumerator ExecuteFunctions(FunctionList funcs)
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
            if (timePassed - nodeScriptLine.TimeSec < 0)
            {
                var waitTime = nodeScriptLine.TimeSec - timePassed;
                yield return new WaitForSeconds(waitTime);
            }

            Debug.Log($"Node Function: {nodeScriptLine.Function?.GetType().FullName} at {timePassed}");
            nodeScriptLine.Function?.RaiseEvent();
        }

        FunctionsFinished = true;
    }

    private static float TimePassed(float timeStarted)
    {
        return Time.timeSinceLevelLoad - timeStarted;
    }
}