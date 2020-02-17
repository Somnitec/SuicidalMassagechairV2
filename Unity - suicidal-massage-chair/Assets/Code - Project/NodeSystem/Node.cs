using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu]
public class Node : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public AudioClip AudioClip;
    public List<NodeScriptLine> functions;

    public IEnumerator InvokeFunctions()
    {
        var sorted = functions.OrderBy(a => a.Time);

        float timeStarted = Time.timeSinceLevelLoad;

        foreach (var nodeScriptLine in sorted)
        {
            var timePassed = Time.timeSinceLevelLoad - timeStarted;
            Debug.Log($"TimePassed: {timePassed} node {nodeScriptLine.Time} wait? {nodeScriptLine.Time < timePassed}");
            if (nodeScriptLine.Time < timePassed)
            {
                var waitTime = nodeScriptLine.Time - timePassed;
                Debug.Log($"Starting wait of {waitTime} timePassed {timePassed}");
                yield return new WaitForSeconds(waitTime);
            }

            Debug.Log($"Executing {nodeScriptLine.function.GetType().FullName} at {timePassed}");
            ScriptEvent.Instance.Raise(nodeScriptLine.function);
        }
    }
}