using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


public class BaseNode : SerializableNode
{
    [Input] public Connection Input;
    public NodeGraph NodeGraph => (NodeGraph)graph;

    public virtual void OnNodeEnable() { }
    public virtual void OnNodeDisable() { }

    [ContextMenu("Set Current")]
    public void SetCurrent()
    {
        NodeGraph.SetCurrentNodeTo(this);
    }

    [ContextMenu("Set Root")]
    public void SetRoot()
    {
        NodeGraph.SetRoot(this);
    }
}

[Serializable]
public class Connection
{
}

[ShowOdinSerializedPropertiesInInspector]
public class SerializableNode : XNode.Node, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private SerializationData serializationData;

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

