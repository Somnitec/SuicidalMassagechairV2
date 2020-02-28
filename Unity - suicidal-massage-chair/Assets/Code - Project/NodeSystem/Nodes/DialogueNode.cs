using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Event = Framework.Event;


public class DialogueNode : BaseNode
{
    [ShowIf("showData")] [InlineProperty, HideLabel, HideReferenceObjectPicker] [Title("Data")]
    public NodeData Data = new NodeData();

    [ShowIf("showDebugInfo")] [InlineProperty, HideLabel, HideReferenceObjectPicker]
    public NodeLogic Logic = new NodeLogic();

    [Output(dynamicPortList = true), ListDrawerSettings(ShowIndexLabels = false)]
    public List<UserInputButton> Buttons = new List<UserInputButton>();

    [Output] public Connection OnAnyButton;

    private bool showDebugInfo => SettingsHolder.Instance.Settings.ShowNodeDebugInfo;
    private bool showData => SettingsHolder.Instance.Settings.ShowNodeData;
    private bool logDebugInfo => SettingsHolder.Instance.Settings.LogDebugInfo;

    private NodePort AnyButtonPort => GetOutputPort("OnAnyButton");

    /// <summary>
    /// OnEnable -> Wait for Audio & Functions -> Onfinished
    /// OnFinished -> Connected? Wait for Input | else | Graph.NoMoreConnections()
    /// Input After Finished -> Go To Next node
    /// </summary>
    public override void OnNodeEnable()
    {
        if (logDebugInfo)
            Debug.Log($"OnNodeEnable {name}");

        Events.Instance.AddListener<UserInputUp>(OnInterrupted);

        NodeFunctionRunner.Instance.StopAllCoroutines();
        NodeFunctionRunner.Instance.StartCoroutine(
            Logic.InvokeFunctionsAndPlayAudioCoroutine(
                name,
                Data.AudioClip,
                Data.FunctionList,
                OnFinished));
    }

    private void OnInterrupted(UserInputUp e)
    {
        if (logDebugInfo)
            Debug.Log($"OnInterrupted {name} {e.Button}");
        Events.Instance.Raise(new InterruptedInput(e.Button));
    }

    private void OnFinished()
    {
        if(logDebugInfo)
            Debug.Log($"OnFinished {name}");

        if (NodeFinished())
        {
            return;
        }

        Events.Instance.RemoveListener<UserInputUp>(OnInterrupted);
        Events.Instance.AddListener<UserInputUp>(HandleInput);
    }

    private void HandleInput(UserInputUp e)
    {
        Debug.Log($"OnFinished {name}");

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i].HasFlag(e.Button))
            {
                if (logDebugInfo)
                    Debug.Log($"Found Button in Buttons {e.Button}");

                GoToNode(GetButtonPort(i));
                return;
            }
        }
        if (logDebugInfo)
            Debug.Log($"Found Button in AnyButton {e.Button}");
        GoToNode(AnyButtonPort);
    }

    public override void OnNodeDisable()
    {
        if (logDebugInfo)
            Debug.Log($"OnNodeDisable {name}");

        Events.Instance.RemoveListener<UserInputUp>(HandleInput);
    }

    public override bool HasConnections()
    {
        var onAnyButtonConnected = AnyButtonPort.IsConnected;
        var anyButtonsConnected = AnyButtonConnected();
        if (logDebugInfo)
            Debug.Log($"HasConnections {onAnyButtonConnected} {anyButtonsConnected}");

        return onAnyButtonConnected || anyButtonsConnected;
    }

    private bool AnyButtonConnected()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (GetButtonPort(i).IsConnected)
                return true;
        }
        return false;
    }

    private NodePort GetButtonPort(int i)
    {
        return GetOutputPort($"Buttons {i}");
    }

    private void GoToNode(NodePort port)
    {
        if (!port.IsConnected)
        {
            Debug.LogWarning($"{port.fieldName} is not connected to anything for node {name}");
            return;
        }

        var node = (BaseNode)port.Connection.node;
        NodeGraph.PlayNode(node);
    }
}

public class InterruptedInput : Event
{
    public UserInputButton Button;

    public InterruptedInput(UserInputButton button)
    {
        Button = button;
    }
}