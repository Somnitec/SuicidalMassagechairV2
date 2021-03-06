﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using XNode;


public abstract class BaseNode : SerializableNode
{
    [Input] public Connection Input;
    public NodeGraph NodeGraph => (NodeGraph) graph;

    [ColorPalette()] [HideLabel] [ShowIf("showColors")]
    public Color Color = Color.gray;

    private bool showColors => SettingsHolder.Instance.Settings.ShowColors;

    public virtual void OnNodeEnable()
    {
    }

    public virtual void OnNodeDisable()
    {
    }

    protected abstract bool HasConnections();

    protected bool NodeFinishedNoMoreConnections()
    {
        if (HasConnections()) return false;

        NodeGraph.NoMoreConnections();
        return true;
    }

    protected void GoToNode(NodePort port)
    {
        if (!port.IsConnected)
        {
            Debug.LogWarning(
                $"Trying to go to next node via {port.fieldName} on node:{name}. However it is not connected to anything. :(");
            return;
        }

        var node = (BaseNode) port.Connection.node;
        NodeGraph.PlayNode(node);
    }

    [ContextMenu("Set Current")]
    public void SetCurrent()
    {
        NodeGraph.PlayNode(this);
    }

    [ContextMenu("Set Root")]
    public void SetRoot()
    {
        NodeGraph.SetRoot(this);
    }

    [ContextMenu("Set Special/Kill")]
    public void SetKill()
    {
        SetOrAddSpecialNode(Settings.KillNodeName);
    }
    
    [ContextMenu("Set Special/Interrupted")]
    public void SetInterrupted()
    {
        SetOrAddSpecialNode(Settings.InteruptedNodeName);
    }
    
    [ContextMenu("Set Special/NoInput")]
    public void SetNoInput()
    {
        SetOrAddSpecialNode(Settings.NoInputNodeName);
    }
    
    [ContextMenu("Set Special/Settings")]
    public void SetSettings()
    {
        SetOrAddSpecialNode(Settings.SettingsNodeName);
    }

    private void SetOrAddSpecialNode(string name)
    {
        if (NodeGraph.SpecialNodes.ContainsKey(name))
            NodeGraph.SpecialNodes[name] = this;
        else
            NodeGraph.SpecialNodes.Add(name, this);
    }
}

[Serializable]
public class Connection
{
}

[ShowOdinSerializedPropertiesInInspector]
public class SerializableNode : XNode.Node, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector] private SerializationData serializationData;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        this.OnAfterDeserialize();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        this.OnBeforeSerialize();
        UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
    }

    /// <summary>
    /// Invoked after deserialization has taken place.
    /// </summary>
    protected virtual void OnAfterDeserialize()
    {
    }

    /// <summary>
    /// Invoked before serialization has taken place.
    /// </summary>
    protected virtual void OnBeforeSerialize()
    {
    }
}