using System.Collections;
using System.Collections.Generic;
using Framework;
using Messaging.Raw;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;

[ExecuteInEditMode]
public class InputMicrocontrollerMessager : Messager
{
    protected override void OnMessageReceived(string message)
    {
        RawInput status = InputMessageParser.ParseMessage(message);
        Debug.Log(status.buttonPressed);
        Events.Instance.Raise(new InputUpdate());
    }
}

public class InputUpdate : Event
{
}