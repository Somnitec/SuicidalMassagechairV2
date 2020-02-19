using System;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;


public class DialogueNode : BaseNode
{
    public NodeData Data;

    [Output(dynamicPortList = true), HideLabel]
    public List<UserInputButton> Buttons;

    [Output]
    public Connection OnAnyButton;

    public override void OnNodeEnable()
    {
        Debug.Log("OnNodeEnable");

        Events.Instance.AddListener<UserInputUp>(OnInterrupted);
        Data.OnFinished += OnFinished;
    }

    private void OnInterrupted(UserInputUp e)
    {
        Debug.Log($"OnInterrupted {e.Button}");
    }

    private void OnFinished()
    {
        Debug.Log("OnFinished");

        Events.Instance.AddListener<UserInputUp>(OnInterrupted);
        Events.Instance.AddListener<UserInputUp>(HandleInput);

    }

    private void HandleInput(UserInputUp e)
    {
        Debug.Log("OnFinished");

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i] == e.Button)
            {
                Debug.Log($"Found Button in Buttons {e.Button}");

                // nodeGraph.SetNode(this);
                return;
            }
        }

        Debug.Log($"Found Button in AnyButton {e.Button}");
        // nodeGraph.SetNode(this);

    }


    public override void OnNodeDisable()
    {
        Debug.Log("OnNodeDisable");

        if (Data.OnFinished != null)
            Data.OnFinished -= OnFinished;

        Events.Instance.RemoveListener<UserInputUp>(HandleInput);
    }
}

