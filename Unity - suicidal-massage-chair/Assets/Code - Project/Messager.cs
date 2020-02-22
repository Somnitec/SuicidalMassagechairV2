using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using Event = Framework.Event;

[RequireComponent(typeof(SerialController))]
public abstract class Messager : MessageListener
{
    [SerializeField]
    [ReadOnly]
    [ShowInInspector]
    protected bool ArduinoConnected;

    protected SerialController serialController;

    [SerializeField, PropertyOrder(10)]
    [TextArea(10, 20)]
    protected string MessagesReceived;

    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [PropertySpace]
    [Button]
    public override void MessageFromArduino(string message)
    {
        MessagesReceived = message + "\n" + MessagesReceived;

        UpdateConsoleMessage();

        OnMessageReceived(message);
    }

    protected abstract void OnMessageReceived(string message);

    public override void ConnectionEventFromArduino(bool success)
    {
        ArduinoConnected = success;
    }

    [Button]
    public override void SendMessageToArduino(string message)
    {
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    [Button]
    public override void SendMessageToArduino(string param, int value)
    {
        var message = MessageHelper.ToJson(param, value);
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    [Button]
    public void ClearConsole()
    {
        MessagesReceived = "";

        UpdateConsoleMessage();
    }

    protected void UpdateConsoleMessage()
    {
        Events.Instance.Raise(new ConsoleMessage($"Chair console: \n{MessagesReceived}"));
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