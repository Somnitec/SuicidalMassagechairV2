using System.Collections;
using System.Collections.Generic;
using Framework;
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
    
    [OdinSerialize,
     DictionaryDrawerSettings(
         DisplayMode = DictionaryDisplayOptions.OneLine, 
         IsReadOnly = false,
         KeyLabel = "Name", 
         ValueLabel = "Special Node")]
    public Dictionary<string, BaseNode> SpecialNodes = new Dictionary<string, BaseNode>();

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
    public void InputEvent(UserInputButton button)
    {
        Events.Instance.Raise(new UserInputUp(button));
    }

    [PropertySpace]
    [Button]
    public void PlayRoot()
    {
        PlayNode(RootNode);
    }

    public void PlaySpecialNode(string key)
    {
        if (!SpecialNodes.ContainsKey(key))
        {
            Debug.LogError($"No Special node found for index {key}");
            return;
        }

        PlayNode(SpecialNodes[key]);
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