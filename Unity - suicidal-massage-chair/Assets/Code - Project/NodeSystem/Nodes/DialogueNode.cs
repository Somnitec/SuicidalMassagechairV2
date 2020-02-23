using System;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;


public class DialogueNode : BaseNode
{
    [ShowIf("showData")] [InlineProperty, HideLabel, HideReferenceObjectPicker] [Title("Data")]
    public NodeData Data = new NodeData();

    [ShowIf("showDebugInfo")] [InlineProperty, HideLabel, HideReferenceObjectPicker]
    public NodeLogic Logic = new NodeLogic();

    [Output(dynamicPortList = true), ListDrawerSettings(ShowIndexLabels = false)]
    public List<UserInputButton> Buttons;

    [Output] public Connection OnAnyButton;

    private bool showDebugInfo => SettingsHolder.Instance.Settings.ShowNodeDebugInfo;
    private bool showData => SettingsHolder.Instance.Settings.ShowNodeData;

    public override void OnNodeEnable()
    {
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
        Debug.Log($"OnInterrupted {name} {e.Button}");
    }

    private void OnFinished()
    {
        Debug.Log($"OnFinished {name}");

        Events.Instance.AddListener<UserInputUp>(OnInterrupted);
        Events.Instance.AddListener<UserInputUp>(HandleInput);
    }

    private void HandleInput(UserInputUp e)
    {
        Debug.Log($"OnFinished {name}");

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i] == e.Button)
            {
                Debug.Log($"Found Button in Buttons {e.Button}");

                SetGraphNode($"Buttons {i}");
                return;
            }
        }

        Debug.Log($"Found Button in AnyButton {e.Button}");
        SetGraphNode("OnAnyButton");
    }

    private void SetGraphNode(String portName)
    {
        var port = GetOutputPort(portName);
        if (!port.IsConnected)
        {
            Debug.LogWarning($"{portName} is not connected to anything for node {name}");
            return;
        }

        var node = (BaseNode) port.Connection.node;
        NodeGraph.PlayNode(node);
    }

    public override void OnNodeDisable()
    {
        Debug.Log($"OnNodeDisable {name}");

        Events.Instance.RemoveListener<UserInputUp>(HandleInput);
    }
}