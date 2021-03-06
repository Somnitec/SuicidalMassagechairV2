﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
//using XNode.Examples.StateGraph;
using XNodeEditor;


[CustomNodeEditor(typeof(BaseNode))]
public class BaseNodeEditor : NodeEditor
{
    private const string rootText = "Root: ";
    private const string currentText = "Current: ";
    private const string empty = "";

    public override void OnHeaderGUI()
    {
        GUI.color = Color.white;
        BaseNode node = target as BaseNode;
        var graph = node.NodeGraph;
        bool isCurrent = graph.Current == node;
        bool isRoot = graph.RootNode == node;
        var key = graph.SpecialNodes.FirstOrDefault(x => x.Value == node).Key;
        string specialName = key?.ToString() ?? "";
        if (isCurrent) GUI.color = Color.blue;
        string title = $"{(isRoot ? rootText : empty)}" +
                       $"{(isCurrent ? currentText : empty)}" +
                       $"{(specialName!="" ? "Special[" + specialName + "] : " : "")}" +
                       $"{node.name}";
        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        GUI.color = Color.white;
    }

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        BaseNode node = target as BaseNode;
        var graph = node.NodeGraph;
        if (GUILayout.Button("Set as current")) graph.PlayNode(node);
    }

    public override Color GetTint()
    {
        BaseNode node = target as BaseNode;
        return node.Color;
    }
}  