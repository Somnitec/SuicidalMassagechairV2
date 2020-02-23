using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRootGraph : MonoBehaviour
{
    public NodeGraph Graph;

    void Start()
    {
        Graph.PlayRoot();
    }
}
