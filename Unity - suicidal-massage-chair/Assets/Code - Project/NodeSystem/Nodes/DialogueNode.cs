using System;
using System.Collections.Generic;
using Framework;
using Input;
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
    public List<NormalInputButtons> Buttons = new List<NormalInputButtons>();

    [Output] public Connection OnAnyButton;

    [HideInInspector] public bool SkipAudio = false;
    
    [SerializeField, InlineProperty, HideReferenceObjectPicker, HideLabel]
    private NodeTimeOutLogic timeOutLogic = new NodeTimeOutLogic();

    [ShowIf("hasTimeOut")] [Output] public Connection OnTimeOut;

    private Settings settings => SettingsHolder.Instance.Settings;
    private bool showDebugInfo => settings.ShowNodeDebugInfo;
    private bool showData => settings.ShowNodeData;
    private bool logDebugInfo => settings.LogDebugInfo;
    private bool hasTimeOut => timeOutLogic.HasTimeOut;

    private NodePort anyButtonPort => GetOutputPort("OnAnyButton");
    private NodePort timeOutPort => GetOutputPort("OnTimeOut");

    #endregion

    #region Main Flow

    public override void OnNodeEnable()
    {
        if (logDebugInfo)
            Debug.Log($"OnNodeEnable {name}");

        AudioManager.Instance.Stop();
        
        if (SkipAudio)
        {
            OnFinished();
            return;
        }
        
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

    private void HandleInput(NormalInput e)
    {
        if (logDebugInfo)
            Debug.Log($"HandleInput {name}");
        
        if (ConnectedButtonPressed(e)) return;

        GoToAnyButtonPort(e);
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
        playingLogic.PlayFunctionsAndAudio(OnFinished, Data.Data.AudioClip, Data.Data.FunctionList, name, NodeFunctionRunner.Instance);
    }

    private void StartTimeOut()
    {
        timeOutLogic.StartTimeOut(port => GoToNode(port), NodeFunctionRunner.Instance, this);
    }

    private void WaitForInput()
    {
        Events.Instance.RemoveListener<NormalInput>(OnInterrupted);
        Events.Instance.AddListener<NormalInput>(HandleInput);
        Events.Instance.Raise(new WaitingForInput());
    }

    private void ListenToInterruptedInput()
    {
        Events.Instance.AddListener<NormalInput>(OnInterrupted);
    }

    private void OnInterrupted(NormalInput e)
    {
        if (logDebugInfo)
            Debug.Log($"OnInterrupted {name} {e.Input}");
        Events.Instance.Raise(new InterruptedInput(e.Input));
    }

    private void CleanUp()
    {
        Events.Instance.RemoveListener<NormalInput>(HandleInput);
        NodeFunctionRunner.Instance.StopAllCoroutines();
    }

    #endregion

    #region Helpers

    protected override bool HasConnections()
    {
        var onAnyButtonConnected = anyButtonPort.IsConnected;
        var timeoutConnected = hasTimeOut && timeOutPort.IsConnected;
        var anyButtonsConnected = AnyButtonConnected();

        var hasConnection = onAnyButtonConnected || anyButtonsConnected || timeoutConnected;

        if(!hasConnection)
            Debug.LogWarning("No Connections for DialogueNode");

        return hasConnection;
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

    private void GoToAnyButtonPort(NormalInput e)
    {
        if (logDebugInfo)
            Debug.Log($"Found Button in NormalInput {e.Input}");

        GoToNode(anyButtonPort);
    }

    private bool ConnectedButtonPressed(NormalInput e)
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i].HasFlag(e.Input))
            {
                if (logDebugInfo)
                    Debug.Log($"Found Button in Buttons {e.Input}");

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
    public NormalInputButtons Input;

    public InterruptedInput(NormalInputButtons input)
    {
        Input = input;
    }
}