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
        Chair_Up,
        Chair_Down,
        Reset
        // TODO
    }

    // Todo rethink this in relation to arguments
    // Possibly event pattern?
    public void SendCommand(Commands command)
    {
        Debug.Log("The " + command.ToString() + " has been send");
        // switch (command)
        // {
        //     case Commands.Reset:
        //         ChairEvents.Instance.Raise(new ChairReset());
        //         break;
        //     default:
        //         Debug.LogWarning(command.ToString() + " has no implementation yet");
        //         break;
        // }
    }
}
