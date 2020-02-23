using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayRootGraph : MonoBehaviour
{
    public NodeGraph Graph;

    void Start()
    {
        Graph.PlayRoot();
    }

    [Button]
    public void Restart()
    {
        Graph.PlayRoot();
    }
}
