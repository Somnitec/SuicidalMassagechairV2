using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class NodeGraph : XNode.NodeGraph
{
    public BaseNode RootNode;
    public BaseNode Current;

    [PropertySpace]
    [Button]
    public void PlayNode(BaseNode node)
    {
        Current?.OnNodeDisable();
        Current = node;
        Current?.OnNodeEnable();
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
}