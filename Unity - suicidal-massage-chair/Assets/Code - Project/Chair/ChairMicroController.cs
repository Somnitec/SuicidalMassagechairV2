using System.Collections;
using System.Collections.Generic;
using Messaging;
using UnityEditor;
using UnityEngine;

public class ChairMicroController : MonoBehaviour
{
    [SerializeField]
    private ChairMicroControllerMock mockController;
    [SerializeField]
    private ChairMicroControllerArduino arduinoController;

    private Settings settings => SettingsHolder.Instance.Settings;

    private void Start()
    {
        mockController = new ChairMicroControllerMock(settings.Mock);
        arduinoController = new ChairMicroControllerArduino(settings.Arduino, GetComponent<ChairMicroControllerMessager>());
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
