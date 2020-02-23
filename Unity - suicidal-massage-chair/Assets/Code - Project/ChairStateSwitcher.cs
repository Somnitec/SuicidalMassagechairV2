using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class ChairStateSwitcher : MonoBehaviour
{
    private ChairOutputVisualizer visualizer;
    private Settings settings => SettingsHolder.Instance.Settings;

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
        visualizer.State = settings.Mock;
    }

    [Button]
    public void SwitchToArduino()
    {
        Setup();
        visualizer.State = settings.Arduino;
    }
}
