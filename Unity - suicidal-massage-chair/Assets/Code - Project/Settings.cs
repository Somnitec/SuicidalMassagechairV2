﻿
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class Settings : SerializedScriptableObject
{
    [Title("Hookups")]
    public NodeGraph Graph;
    public ChairMicroControllerState Mock, Arduino;
    public String RepeatedMessage = "status";

    [Title("Sound Effects")]
    public List<AudioClip> ClickSoundEffects;

    [Title("Toggles")]
    public bool ShowColors = true; // TODO implement this
    public bool ShowNodeDebugInfo = true;
    public bool LogDebugInfo = false;
    public bool ShowNodeData = true;
    [ShowIf("ShowNodeData")]
    public bool ShowNodeAudio = true;
    [ShowIf("ShowNodeData")]
    public bool ShowNodeText = true;
    [ShowIf("ShowNodeData")]
    public bool ShowNodeButtons = true;
    [ShowIf("ShowNodeData")]
    public bool ShowNodeFunctions = true;
    public Language Language = Language.Dutch;

    [Title("App setting")]
    public bool ResetChairOnStart = false;
    public float TimeOutTimeInSeconds = 600;

    [Title("Restart", bold:false)]
    [InfoBox("This gets triggered every time the story ends and it restarts.")]
    [HideReferenceObjectPicker, HideLabel, InlineProperty]
    public FunctionList RestartChair = new FunctionList();

    [Title("Waiting for start", bold: false)]
    [InfoBox("This audio and functions play while waiting for any input.")]
    [HideReferenceObjectPicker, HideLabel, InlineProperty]
    public AudioClip WaitingAudio;
    [HideReferenceObjectPicker, HideLabel, InlineProperty]
    public FunctionList WaitingFunctions = new FunctionList();

    [Title("On Start", bold: false)]
    [InfoBox("This gets triggered after a person has pressed a button in the wait mode.")]
    [HideReferenceObjectPicker, HideLabel, InlineProperty]
    public FunctionList OnStart = new FunctionList();

    [Title("Special Node Names")] 
    public static readonly string KillNodeName = "Kill Node";
    public static readonly string InteruptedNodeName = "Interrupted Node";
    public static readonly string NoInputNodeName = "No Input Node";
    public static readonly string SettingsNodeName = "Settings Node";
}

public enum Language
{
    Dutch,
    English
}