using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChairMicroController : MonoBehaviour
{
    public ChairMicroControllerState ArduinoState;
    public ChairMicroControllerState MockState;
    private ChairMicroControllerMock chairMicroController;
    private ChairMicroControllerArduino arduino;

    private void Start()
    {
        if(MockState == null)
            MockState = new ChairMicroControllerState();
        if (ArduinoState == null)
            ArduinoState = new ChairMicroControllerState();

        chairMicroController = new ChairMicroControllerMock(MockState);
        arduino = new ChairMicroControllerArduino(ArduinoState);
    }

    private void OnDisable()
    {
        chairMicroController.RemoveListeners();
    }

    public enum Commands
    {
        ChairUp,
        ChairDown,
        Reset,
    }
}
