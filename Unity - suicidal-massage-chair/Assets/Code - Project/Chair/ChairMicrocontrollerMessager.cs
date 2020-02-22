using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;

[ExecuteInEditMode]
public class ChairMicrocontrollerMessager : MessageListener
{
    [SerializeField] [ReadOnly] [ShowInInspector]
    private bool ArduinoConnected;

    [SerializeField, PropertyOrder(10)] [TextArea(10, 20)]
    private string MessagesReceived;

    private SerialController serialController;

    // Use this for initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    public override void ConnectionEventFromArduino(bool success)
    {
        ArduinoConnected = success;
    }

    [Button]
    public override void MessageFromArduino(string message)
    {
        MessagesReceived = message + "\n" + MessagesReceived;

        UpdateConsoleMessage();

        RawChairStatus status = ChairMessageParser.ParseMessage(message);
        Events.Instance.Raise(new ChairStateUpdate(status));
    }

    [PropertySpace, Button]
    public void SendMessageToArduino(string message)
    {
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    [Button]
    public void ClearConsole()
    {
        MessagesReceived = "";

        UpdateConsoleMessage();
    }

    private void UpdateConsoleMessage()
    {
        Events.Instance.Raise(new ConsoleMessage($"Chair console: \n{MessagesReceived}"));
    }
}

public class ChairStateUpdate : Event
{
    public RawChairStatus state { get; }

    public ChairStateUpdate(RawChairStatus state)
    {
        this.state = state;
    }
}

public class ConsoleMessage : Event
{
    public string Text;

    public ConsoleMessage(string msg)
    {
        Text = msg;
    }
}