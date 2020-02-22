using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeData
{
    // add Language?
    [HideLabel] [AssetsOnly] public AudioClip AudioClip;
    [TextArea, PropertySpace(0, 10f)] public string Text;

    [TableList(AlwaysExpanded = true, HideToolbar = true)]
    [HideLabel]
    [FoldoutGroup("Chair Functions")]
    [PropertyOrder(10)]
    public List<NodeScriptLine> Functions = new List<NodeScriptLine>();

    [HideInInspector] public Action OnFinished;

    private bool audioFinished = false;
    private bool functionsFinished = false;

    public IEnumerator InvokeFunctions()
    {
        float timeStarted = Time.timeSinceLevelLoad;

        functionsFinished = false;
        audioFinished = false;

        if (AudioClip == null)
        {
            Debug.LogWarning($"No audioClip on node");
        }
        else
        {
            AudioManager.Instance.PlayClip(AudioClip, () => audioFinished = true);
        }

        yield return ExecuteFunctions(timeStarted);

        while (!audioFinished || !functionsFinished)
            yield return null;

        OnFinished?.Invoke();
    }

    private IEnumerator ExecuteFunctions(float timeStarted)
    {
        Sort();

        foreach (var nodeScriptLine in Functions)
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

        functionsFinished = true;
    }

    private static float TimePassed(float timeStarted)
    {
        return Time.timeSinceLevelLoad - timeStarted;
    }

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    private void Sort()
    {
        Functions = Functions.OrderBy(a => a.TimeSec).ToList();
    }

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    private void Add()
    {
        Functions.Add(new NodeScriptLine());
    }
}