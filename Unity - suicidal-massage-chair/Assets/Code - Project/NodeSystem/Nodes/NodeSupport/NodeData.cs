using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [ValidateInput("Validate", "Max length is 44 chars", InfoMessageType.Error)]
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
        return input == null || input?.Length < 44;
    }
}

public class FunctionList
{
    [TableList(AlwaysExpanded = true, HideToolbar = true)]
    [HideLabel]
    [FoldoutGroup("Chair Functions")]
    [PropertyOrder(10)]
    public List<NodeScriptLine> Functions = new List<NodeScriptLine>();

    public float Duration => Functions.Max(f => f.TimeSec as float?) ?? 0;

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    public void Sort()
    {
        Functions = Functions.OrderBy(a => a.TimeSec).ToList();
    }

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    private void Add()
    {
        Functions.Add(new NodeScriptLine());
    }
}