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
    public AudioClip AudioClip;
    [TextArea] public string Description;

    [OnValueChanged("Sort",true)]
    [TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 10, ShowPaging = true)]
    [ListDrawerSettings]
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
        Sort();

        foreach (var nodeScriptLine in Functions)
        {
            var timePassed = TimePassed(timeStarted);
            if (timePassed - nodeScriptLine.TimeInSec < 0)
            {
                var waitTime = nodeScriptLine.TimeInSec - timePassed;
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

    private void Sort()
    {
        Functions = Functions.OrderBy(a => a.TimeInSec).ToList();
    }
}