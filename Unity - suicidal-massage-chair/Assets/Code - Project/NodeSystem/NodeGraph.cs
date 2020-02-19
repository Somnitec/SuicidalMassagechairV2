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

    [Button]
    public void SetNode(BaseNode node)
    {
        Current?.OnNodeDisable();
        Current = node;
        Current?.OnNodeEnable();
    }

    [Button]
    public void SetRoot(BaseNode node)
    {
        RootNode = node;
    }

    [Button]
    public void InputEvent(UserInputButton button)
    {
        Events.Instance.Raise(new UserInputUp(button));
    }

    [Button]
    public void StartFromRoot()
    {
        SetNode(RootNode);
    }
}
