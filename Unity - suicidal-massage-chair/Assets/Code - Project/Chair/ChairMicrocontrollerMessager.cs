using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;

[ExecuteInEditMode]
public class ChairMicrocontrollerMessager : SingletonMonoBehavior<ChairMicrocontrollerMessager>
{
    [SerializeField] [ReadOnly] [ShowInInspector]
    private bool ArduinoConnected;
    [SerializeField, PropertyOrder(10)] [TextArea(10,20)]
    private string MessagesReceived;
    private SerialController serialController;

    // Use this for initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [PropertySpace,Button]
    public void SendMessageToArduino(string message)
    {
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        MessagesReceived = msg + "\n" + MessagesReceived;

        Events.Instance.Raise(new ConsoleMessage($"Chair console: \n{MessagesReceived}"));
    }

    void OnConnectionEvent(bool success)
    {
        ArduinoConnected = success;
    }

    [Button]
    public void ClearConsole()
    {
        MessagesReceived = "";

        Events.Instance.Raise(new ConsoleMessage($"Chair console: \n{MessagesReceived}"));
    }
}

public class ChairMessageParser
{
    public void ParseMessage(string msg)
    {

    }
}

public class UpdateChairState : Event
{
    public ChairMicroControllerState State;
}

public class ConsoleMessage : Event
{
    public string Text;

    public ConsoleMessage(string msg)
    {
        Text = msg;
    }
}