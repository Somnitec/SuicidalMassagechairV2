using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class ChairStateSwitcher : MonoBehaviour
{
    public ChairMicroControllerState Arduino, Mock;
    private ChairOutputVisualizer visualizer;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (visualizer == null)
            visualizer = FindObjectOfType<ChairOutputVisualizer>();
    }

    [Button]
    public void SwitchToMock()
    {
        Setup();
        visualizer.State = Mock;
    }

    [Button]
    public void SwitchToArduino()
    {
        Setup();
        visualizer.State = Arduino;
    }
}
