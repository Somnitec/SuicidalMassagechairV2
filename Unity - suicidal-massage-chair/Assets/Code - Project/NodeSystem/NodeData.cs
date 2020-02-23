using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeLogic
{
    [HideInInspector] public Action OnFinished;

    [Title("Debug Info")]
    [ReadOnly, ShowInInspector] public bool AudioFinished { get; private set; }
    [ReadOnly, ShowInInspector] public bool FunctionsFinished { get; private set; }

    public IEnumerator InvokeFunctionsAndPlayAudio(string Name, NodeData data)
    {
        float timeStarted = Time.timeSinceLevelLoad;
        FunctionsFinished = false;
        AudioFinished = false;
        if (data.AudioClip == null)
        {
            Debug.LogWarning($"No audioClip on data of {Name}");
            AudioFinished = true;
        }

        else
        {
            AudioManager.Instance.PlayClip(data.AudioClip, () => AudioFinished = true);
        }

        yield return ExecuteFunctions(timeStarted, data);
        while (!AudioFinished || !FunctionsFinished)
            yield return null;
        OnFinished?.Invoke();
    }

    private IEnumerator ExecuteFunctions(float timeStarted, NodeData data)
    {
        data.Sort();

        foreach (var nodeScriptLine in data.Functions)
        {
            var timePassed = TimePassed(timeStarted);
            if (timePassed - nodeScriptLine.TimeSec < 0)
            {
                var waitTime = nodeScriptLine.TimeSec - timePassed;
                // Debug.Log($"Starting wait of {waitTime} timePassed {timePassed}");
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