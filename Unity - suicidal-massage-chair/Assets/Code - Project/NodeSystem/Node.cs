using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Change this to NodeData 
// Split language specific part away from functions
[CreateAssetMenu]
public class Node : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public AudioClip AudioClip;
    public List<NodeScriptLine> Functions;

    public IEnumerator InvokeFunctions()
    {
        var sorted = Functions.OrderBy(a => a.Time);

        float timeStarted = Time.timeSinceLevelLoad;

        foreach (var nodeScriptLine in sorted)
        {
            var timePassed = Time.timeSinceLevelLoad - timeStarted;
            Debug.Log($"TimePassed: {timePassed} node {nodeScriptLine.Time} wait? {nodeScriptLine.Time < timePassed}");
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
}