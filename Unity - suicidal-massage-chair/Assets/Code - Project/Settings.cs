
using System;
using System.Collections.Generic;
using NodeSystem.Functions;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class Settings : SerializedScriptableObject
{
    [Title("Hookups")]
    public NodeGraph Graph;
    public ChairMicroControllerState Mock, Arduino;
    public CompositeFunctionLibrary compositeFunctionLibrary;
    public String RepeatedMessage = "status";

    [Title("Sound Effects")]
    public List<AudioClip> ClickSoundEffects;

    [Title("Toggles")]
    public bool ShowColors = true; // TODO implement this
    public bool ShowNodeDebugInfo = true;
    public bool LogDebugInfo = false;
    public bool ShowNodeData = true;
    [ShowIf("ShowNodeData"), Indent()]
    public bool ShowNodeAudio = true;
    [ShowIf("ShowNodeData"), Indent()]
    public bool ShowNodeText = true;
    [ShowIf("ShowNodeData"), Indent()]
    public bool ShowNodeButtons = true;
    [ShowIf("ShowNodeData"), Indent()]
    public bool ShowNodeFunctions = true;

    [Title("App setting")]
    public Language Language = Language.Dutch;
    public bool ResetChairOnStart = false;
    public float TimeOutTimeInSeconds = 600;
    [InfoBox("The amount of interruptions initially needed before it goes to the Special Interrupted Node")]
    public int InitialInterruptCountBeforeGoingToSpecialNode = 3;
    [InfoBox("The amount of time initially needed before it goes to the Special TimeOut Node in seconds")]
    public float InitialTimeOutBeforeGoingToSpecialNode = 10f;
    public List<float> VolumeLevels = new List<float>()
    {
        0.2f,
        0.4f,
        0.6f,
        0.8f,
        1.0f,
    };

    [Title("Restart", bold:false)]
    [InfoBox("This gets triggered every time the story ends and it restarts.")]
    public FunctionList RestartChair = new FunctionList();

    [Title("Waiting for start", bold: false)]
    [InfoBox("This audio and functions play while waiting for any input.")]
    [HideReferenceObjectPicker, HideLabel, InlineProperty]
    public AudioClip WaitingAudio;
    public FunctionList WaitingFunctions = new FunctionList();

    [Title("Before Start", bold: false)]
    [InfoBox("This gets triggered after a person has pressed a button in the wait mode. After this is complete, play root node.")]
    public FunctionList OnStart = new FunctionList();

    [Title("After Start", bold: false)]
    [InfoBox("This starts playing after the first node has been triggered.")]
    public FunctionList AfterStart;
    
    [Title("Special Node Names")] 
    public static readonly string KillNodeName = "Kill Node";
    public static readonly string InteruptedNodeName = "Interrupted Node";
    public static readonly string NoInputNodeName = "No Input Node";
    public static readonly string SettingsNodeName = "Settings Node";
    
    [Title("Special Blackboard Names")] 
    public static readonly string InterruptedCountBBName = "Interrupted Count";
    public static readonly string InterruptionsHandledBBName = "Interruptions Handled";
    public static readonly string InterruptionsBeforeGoingToNodeBBName = "Interruptions Before Going To Node";
    public static readonly string NoInputCounterBBName =  "NoInput - Time Out Counter";
    public static readonly string NoInputGoToNodeBBName =  "NoInput - Go To Node Time";
    public static readonly string NoInputCanGotToNodeBBName = "NoInput - Can Go To Node";
    public static readonly string MasterVolumeName = "MasterVolume";
}

public enum Language
{
    Dutch,
    English
}