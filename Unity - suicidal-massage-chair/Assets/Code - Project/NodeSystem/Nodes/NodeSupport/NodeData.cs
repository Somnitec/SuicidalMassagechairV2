﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeData
{
    [HideLabel] [AssetsOnly]
    public AudioClip AudioClip;
    [TextArea(10, 20)]
    [PropertySpace(0, 10f)]
    public string Text;

    [InlineProperty,HideLabel,HideReferenceObjectPicker]
    public FunctionList FunctionList = new FunctionList();
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