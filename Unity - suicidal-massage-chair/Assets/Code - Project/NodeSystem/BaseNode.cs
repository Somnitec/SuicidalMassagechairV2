using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


public abstract class BaseNode : SerializableNode
{
    [Input] public Connection Input;
    public NodeGraph NodeGraph => (NodeGraph)graph;
    [ColorPalette()][HideLabel][ShowIf("showColors")]
    public Color Color = Color.gray;

    private bool showColors => SettingsHolder.Instance.Settings.ShowColors;

    public virtual void OnNodeEnable() { }
    public virtual void OnNodeDisable() { }

    public abstract bool HasConnections();

    protected bool NodeFinished()
    {
        if (!HasConnections())
        {
            NodeGraph.NoMoreConnections();
            return true;
        }

        return false;
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
        NodeGraph.SpecialNodes.Add("KillNode", this);
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

