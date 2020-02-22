using System;
using System.Collections;
using System.Collections.Generic;
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
        if (isCurrent) GUI.color = Color.blue;
        string title = $"{(isRoot ? rootText : empty)}{(isCurrent ? currentText : empty)}{node.name}";
        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        GUI.color = Color.white;
    }

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        BaseNode node = target as BaseNode;
        var graph = node.NodeGraph;
        if (GUILayout.Button("Set as current")) graph.SetCurrentNodeTo(node);
    }
}  