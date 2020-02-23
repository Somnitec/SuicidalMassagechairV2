
using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class Settings : SerializedScriptableObject
{
    [Title("Hookups")]
    public NodeGraph Graph;
    public ChairMicroControllerState Mock, Arduino;
    public String RepeatedMessage = "status";
    [Title("Toggles")]
    public bool ShowColors = true; // TODO implement this
    public bool ShowNodeDebugInfo = true;
    public bool ShowNodeData = true;
    [Title("App setting")]
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
}
