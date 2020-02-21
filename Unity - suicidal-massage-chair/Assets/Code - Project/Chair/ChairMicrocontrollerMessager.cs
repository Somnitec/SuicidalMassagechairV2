using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[ExecuteInEditMode]
public class ChairMicrocontrollerMessager : SingletonMonoBehavior<ChairMicrocontrollerMessager>
{
    [SerializeField] [ReadOnly] [ShowInInspector]
    private bool ArduinoConnected;
    [ReadOnly, SerializeField, TextArea]
    private string MessagesReceived;
    private SerialController serialController;

    // Use this for initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [PropertySpace]
    [Button]
    public void SendMessage(string message)
    {
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        MessagesReceived += msg + "\n";
        switch (name)
        {
            case "object":
                Debug.Log("staying alive: " + msg);
                break;
            default:
                Debug.Log("no response: " + msg);
                break;
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        // Debug.Log(success ? "Device connected" : "Device disconnected");
        ArduinoConnected = success;
    }
}