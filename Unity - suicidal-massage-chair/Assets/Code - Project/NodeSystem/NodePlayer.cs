using EasyButtons;
using UnityEngine;

public class NodePlayer : MonoBehaviour
{
    public Node node;

    [Button]
    public void PlayNode()
    {
        StartCoroutine(node.InvokeFunctions());
    }
}