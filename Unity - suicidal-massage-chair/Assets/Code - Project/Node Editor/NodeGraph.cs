using System.Collections;
using System.Collections.Generic;
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
}
