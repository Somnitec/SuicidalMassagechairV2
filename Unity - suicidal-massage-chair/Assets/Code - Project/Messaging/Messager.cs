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
    public bool ArduinoConnected;

    protected SerialController serialController;

    [SerializeField, PropertyOrder(10)]
    [TextArea(10, 20)]
    protected string MessagesStorage;

    public int MessagesSent { get; private set; }
    public int MessagesReceived { get; private set; }

    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [PropertySpace]
    [Button]
    public override void MessageFromArduino(string message)
    {
        MessagesStorage = message + "\n" + MessagesStorage;

        MessagesReceived++;

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
        Debug.Log($"Sending to arduino[{(ArduinoConnected ? 0 : 1)}]{gameObject.name}: {message} ");

        if (!ArduinoConnected) return;
        
        serialController.SendSerialMessage(message);
        MessagesSent++;
    }

    [Button]
    public override void SendMessageToArduino(string param, int value)
    {
        var message = MessageHelper.ToJson(param, value);
        SendMessageToArduino(message);
    }

    [Button]
    public void ClearConsole()
    {
        MessagesStorage = "";

        UpdateConsoleMessage();
    }

    protected void UpdateConsoleMessage()
    {
        Events.Instance.Raise(new ConsoleMessage(MessagesStorage, this));
    }
}

public class ConsoleMessage : Event
{
    public string Text;
    public Messager Messager;

    public ConsoleMessage(string msg, Messager messager)
    {
        Text = msg;
        Messager = messager;
    }
}