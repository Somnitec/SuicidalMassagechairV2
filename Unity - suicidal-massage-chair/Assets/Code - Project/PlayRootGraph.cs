using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayRootGraph : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private NodeGraph Graph => settings.Graph;

    void Start()
    {
        if(!settings.ResetChairOnStart)
            Graph.PlayRoot();
    }

    [Button]
    public void Restart()
    {
        Graph.PlayRoot();
    }
}
