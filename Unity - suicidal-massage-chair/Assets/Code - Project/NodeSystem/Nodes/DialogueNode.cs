using System;
using System.Collections.Generic;
using Framework;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;
using Event = Framework.Event;


public class DialogueNode : BaseNode
{
    #region Fields

    [Title("Data")] [ShowIf("showData"), InlineProperty, HideLabel, HideReferenceObjectPicker]
    public NodeMultiLanguageData Data = new NodeMultiLanguageData();

    [ShowIf("showDebugInfo")] [InlineProperty, HideLabel, HideReferenceObjectPicker]
    public NodePlayingLogic playingLogic = new NodePlayingLogic();

    [Output(dynamicPortList = true)] [ListDrawerSettings(ShowIndexLabels = false)]
    public List<UserInputButton> Buttons = new List<UserInputButton>();

    [Output] public Connection OnAnyButton;

    [SerializeField, InlineProperty, HideReferenceObjectPicker, HideLabel]
    private NodeTimeOutLogic timeOutLogic = new NodeTimeOutLogic();

    [ShowIf("hasTimeOut")] [Output] public Connection OnTimeOut;

    private Settings settings => SettingsHolder.Instance.Settings;
    private bool showDebugInfo => settings.ShowNodeDebugInfo;
    private bool showData => settings.ShowNodeData;
    private bool logDebugInfo => settings.LogDebugInfo;
    private bool hasTimeOut => timeOutLogic.HasTimeOut;

    private NodePort AnyButtonPort => GetOutputPort("OnAnyButton");

    #endregion

    #region Main Flow

    public override void OnNodeEnable()
    {
        if (logDebugInfo)
            Debug.Log($"OnNodeEnable {name}");

        ListenToInterruptedInput();
        PlayFunctionsAndAudio(OnFinished);
    }

    private void OnFinished()
    {
        if (logDebugInfo)
            Debug.Log($"OnFinished {name}");

        SendCustomButtonsToInputController();

        if (NodeFinishedNoMoreConnections()) return;

        WaitForInput();
        StartTimeOut();
    }

    private void SendCustomButtonsToInputController()
    {
        Events.Instance.Raise(new CustomButtonTextUpdate(
            Data.Data.CustomButtonA,
            Data.Data.CustomButtonB,
            Data.Data.CustomButtonC
        ));
    }

    private void HandleInput(UserInputUp e)
    {
        if (logDebugInfo)
            Debug.Log($"HandleInput {name}");
        
        if (ConnectedButtonPressed(e)) return;

        if (SpecialButton(e)) return;

        GoToAnyButtonPort(e);
    }

    private bool SpecialButton(UserInputUp userInputUp)
    {
        if (userInputUp.Button.HasFlag(UserInputButton.Kill))
            return true;

        return false;
    }

    public override void OnNodeDisable()
    {
        if (logDebugInfo)
            Debug.Log($"OnNodeDisable {name}");

        CleanUp();
    }

    #endregion

    #region Flow

    private void PlayFunctionsAndAudio(Action onFinished)
    {
        playingLogic.PlayFunctionsAndAudio(OnFinished, Data.Data.AudioClip, Data.Data.FunctionList, name);
    }

    private void StartTimeOut()
    {
        timeOutLogic.StartTimeOut(port => GoToNode(port), NodeFunctionRunner.Instance, this);
    }

    private void WaitForInput()
    {
        Events.Instance.RemoveListener<UserInputUp>(OnInterrupted);
        Events.Instance.AddListener<UserInputUp>(HandleInput);
        Events.Instance.Raise(new WaitingForInput());
    }

    private void ListenToInterruptedInput()
    {
        Events.Instance.AddListener<UserInputUp>(OnInterrupted);
    }

    private void OnInterrupted(UserInputUp e)
    {
        if (logDebugInfo)
            Debug.Log($"OnInterrupted {name} {e.Button}");
        Events.Instance.Raise(new InterruptedInput(e.Button));
    }

    private void CleanUp()
    {
        Events.Instance.RemoveListener<UserInputUp>(HandleInput);
        NodeFunctionRunner.Instance.StopAllCoroutines();
    }

    #endregion

    #region Helpers

    protected override bool HasConnections()
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

    private void GoToAnyButtonPort(UserInputUp e)
    {
        if (logDebugInfo)
            Debug.Log($"Found Button in AnyButton {e.Button}");

        if (!UserInputButton.AnyButton.HasFlag(e.Button))
        {
            Debug.Log($"Ignoring {e.Button}");
            return;
        }

        GoToNode(AnyButtonPort);
    }

    private bool ConnectedButtonPressed(UserInputUp e)
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i].HasFlag(e.Button))
            {
                if (logDebugInfo)
                    Debug.Log($"Found Button in Buttons {e.Button}");

                GoToNode(GetButtonPort(i));
                return true;
            }
        }

        return false;
    }

    #endregion
}

internal class WaitingForInput : Event
{
}

public class InterruptedInput : Event
{
    public UserInputButton Button;

    public InterruptedInput(UserInputButton button)
    {
        Button = button;
    }
}