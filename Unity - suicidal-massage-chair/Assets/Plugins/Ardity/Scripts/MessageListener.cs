using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageListener : SingletonMonoBehavior<MessageListener>
{
    public abstract void ConnectionEventFromArduino(bool success);
    public abstract void MessageFromArduino(string message);
    public abstract void SendMessageToArduino(string message);
}
