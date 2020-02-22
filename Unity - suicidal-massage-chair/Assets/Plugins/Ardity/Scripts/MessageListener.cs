using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class MessageListener : SingletonMonoBehavior<MessageListener>
{
    public abstract void ConnectionEventFromArduino(bool succes);
    public abstract void MessageFromArduino(string message);
    public abstract void SendMessageToArduino(string msg);
    public abstract void SendMessageToArduino(string param, int value);
}