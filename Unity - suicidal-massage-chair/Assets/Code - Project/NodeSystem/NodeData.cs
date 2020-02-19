using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu, InlineEditor()]
public class NodeData : ScriptableObject
{
    // add Language?
    public string Name;
    [TextArea] public string Description;
    public AudioClip AudioClip;
    public List<NodeScriptLine> Functions;

    public Action OnFinished;

    public IEnumerator InvokeFunctions()
    {
        float timeStarted = Time.timeSinceLevelLoad;

        // Play audioclip

        yield return ExecuteFunctions(timeStarted);

        // Wait for audioclip
   
        OnFinished?.Invoke();
    }

    private IEnumerator ExecuteFunctions(float timeStarted)
    {
        var sorted = Functions.OrderBy(a => a.Time);

        foreach (var nodeScriptLine in sorted)
        {
            var timePassed = TimePassed(timeStarted);
            if (timePassed - nodeScriptLine.Time < 0)
            {
                var waitTime = nodeScriptLine.Time - timePassed;
                Debug.Log($"Starting wait of {waitTime} timePassed {timePassed}");
                yield return new WaitForSeconds(waitTime);
            }

            Debug.Log($"Executing {nodeScriptLine.Function.GetType().FullName} at {timePassed}");
            nodeScriptLine.Function.RaiseEvent();
        }
    }

    private static float TimePassed(float timeStarted)
    {
        return Time.timeSinceLevelLoad - timeStarted;
    }
}