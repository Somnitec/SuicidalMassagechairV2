using System;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;


public class DialogueNode : BaseNode
{
    [InlineProperty, HideLabel]
    public NodeData Data = new NodeData();

    [Output(dynamicPortList = true), HideLabel]
    public List<UserInputButton> Buttons;

    [Output]
    public Connection OnAnyButton;

    public override void OnNodeEnable()
    {
        Debug.Log($"OnNodeEnable {name}");

        Events.Instance.AddListener<UserInputUp>(OnInterrupted);
        Data.OnFinished += OnFinished;

        NodeFunctionRunner.Instance.StopAllCoroutines();
        NodeFunctionRunner.Instance.StartCoroutine(Data.InvokeFunctions());
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

        var node = (BaseNode)port.Connection.node;
        NodeGraph.SetNode(node);
    }


    public override void OnNodeDisable()
    {
        Debug.Log($"OnNodeDisable {name}");

        if (Data?.OnFinished != null)
            Data.OnFinished -= OnFinished;

        Events.Instance.RemoveListener<UserInputUp>(HandleInput);
    }
}

