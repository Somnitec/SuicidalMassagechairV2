using System;
using System.Collections;
using NodeSystem.Functions;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeData
{
    [HideLabel] [AssetsOnly]
    [ShowIf("ShowAudio")]
    public AudioClip AudioClip;
    
    [TextArea(10, 20)]
    [PropertySpace(0, 10f)]
    [ShowIf("ShowText")]
    public string Text;

    [TextArea(1, 5)]
    [ValidateInput("Validate", "Max length is 52 chars", InfoMessageType.Error)]
    [ShowIf("ShowButtons")]
    public string CustomButtonA, CustomButtonB, CustomButtonC = "";

    [InlineProperty,HideLabel,HideReferenceObjectPicker]
    [ShowIf("ShowFunctions")]
    public FunctionList FunctionList = new FunctionList();

    private bool ShowText => _settings.ShowNodeText;
    private bool ShowButtons => _settings.ShowNodeButtons;
    private bool ShowAudio => _settings.ShowNodeAudio;
    private bool ShowFunctions => _settings.ShowNodeFunctions;
    private Settings _settings => SettingsHolder.Instance.Settings;
    
    private bool Validate(string input)
    {
        return input == null || input?.Length < 52;
    }
}