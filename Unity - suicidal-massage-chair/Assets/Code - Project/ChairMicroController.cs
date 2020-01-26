using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChairMicroController : MonoBehaviour
{
    public ChairMicroControllerState state;
    private ChairMicroControllerMock chairMicroController;

    private void Start()
    {
        if(state == null)
            state = new ChairMicroControllerState();

        chairMicroController = new ChairMicroControllerMock(state);
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
        switch (command)
        {
            case Commands.Reset:
                ChairEvents.Instance.Raise(new ChairReset());
                break;
            default:
                Debug.LogWarning(command.ToString() + " has no implementation yet");
                break;
        }
    }
}
