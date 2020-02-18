using EasyButtons;
using UnityEngine;

public class NodePlayer : SingletonMonoBehavior<NodePlayer>
{
    public Node Node;

    public void GoToNode(Node node)
    {
        node.Disable();
        this.Node = node;
        node.Enable();
        PlayNode();
    }

    [Button]
    private void PlayNode()
    {
        StartCoroutine(Node.Data.InvokeFunctions());
    }

    [Button]
    private void EnableNode()
    {
        Node.Enable();
    }

    [Button]
    private void OnDataFinished()
    {
        Node.OnDataFinished();
    }

    [Button]
    private void DisableNode()
    {
        Node.Disable();
    }
}