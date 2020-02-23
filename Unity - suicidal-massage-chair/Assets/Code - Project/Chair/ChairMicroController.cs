using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChairMicroController : MonoBehaviour
{
    private ChairMicroControllerMock mockController;
    private ChairMicroControllerArduino arduinoController;

    private Settings settings => SettingsHolder.Instance.Settings;

    private void Start()
    {
        mockController = new ChairMicroControllerMock(settings.Mock);
        arduinoController = new ChairMicroControllerArduino(settings.Arduino);
    }

    private void OnDisable()
    {
        mockController.RemoveListeners();
    }

    public enum Commands
    {
        ChairUp,
        ChairDown,
        Reset,
    }
}
