using System.Collections;
using System.Collections.Generic;
using Framework;
using Input;
using NodeSystem.BlackBoard;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Event = Framework.Event;

[CreateAssetMenu]
public class NodeGraph : SerializedNodeGraph
{
    public BlackBoard BlackBoard;
    public BaseNode RootNode;
    public BaseNode Current;
    [SerializeField]
    private BaseNode goBackNode;

    [OdinSerialize,
     DictionaryDrawerSettings(
         DisplayMode = DictionaryDisplayOptions.OneLine, 
         IsReadOnly = false,
         KeyLabel = "Name", 
         ValueLabel = "Special Node")]
    public Dictionary<string, BaseNode> SpecialNodes = new Dictionary<string, BaseNode>();

    public bool HasGoBackNode => goBackNode != null;
    
    private Settings settings => SettingsHolder.Instance.Settings;
    
    
    [PropertySpace]
    [Button]
    public void PlayNode(BaseNode node)
    {
        if (settings.LogDebugInfo) Debug.Log($"Playing {node.name}");
        Current?.OnNodeDisable();
        Current = node;
        Current?.OnNodeEnable();
        Events.Instance.Raise(new NewNode());
    }

    [Button]
    public void NoMoreConnections()
    {
        Debug.LogError($"No more connections {Current.name}");
        Events.Instance.Raise(new StoryFinished());
    }

    [PropertySpace]
    [Button]
    public void SetRoot(BaseNode node)
    {
        RootNode = node;
    }
    
    [PropertySpace]
    [Button]
    public void PlayRoot()
    {
        PlayNode(RootNode);
    }

    [Button]
    public void PlaySpecialNode(string key)
    {
        if (!SpecialNodes.ContainsKey(key))
        {
            Debug.LogError($"No Special node found for key: {key}");
            return;
        }
        if(goBackNode == null)
            goBackNode = Current;

        PlayNode(SpecialNodes[key]);
    }

    [Button]
    public void PlayGoBackNode()
    {
        if (!HasGoBackNode)
        {
            Debug.LogError($"No Go Back Node has been set");
            return;
        }
        
        PlayNode(goBackNode);
        goBackNode = null;
    }

    public void Reset()
    {
        goBackNode = null;
        BlackBoard.Reset();
    }
}

public class NewNode : Event
{
}

[ShowOdinSerializedPropertiesInInspector]
public class SerializedNodeGraph : XNode.NodeGraph, ISerializationCallbackReceiver
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