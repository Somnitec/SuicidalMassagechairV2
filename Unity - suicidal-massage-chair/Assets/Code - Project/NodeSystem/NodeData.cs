using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class NodeData
{
    // add Language?
    [HideLabel]
    public AudioClip AudioClip;
    [TextArea] public string Text;

    // [OnValueChanged("Sort",true)]
    [TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 10, ShowPaging = true, HideToolbar = true)]
    [HideLabel]

    [PropertyOrder(10)]
    public List<NodeScriptLine> Functions = new List<NodeScriptLine>();

    [HideInInspector]
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

    [PropertySpace]
    [HorizontalGroup("Buttons")]
    [Button]
    private void Sort()
    {
        Functions = Functions.OrderBy(a => a.TimeInSec).ToList();
    }
    [PropertySpace]
    [HorizontalGroup("Buttons")]
    [Button]
    private void Add()
    {
        Functions.Add(new NodeScriptLine());
    }
}