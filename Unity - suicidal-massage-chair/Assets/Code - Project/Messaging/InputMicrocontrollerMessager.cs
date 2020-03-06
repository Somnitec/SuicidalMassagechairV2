using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;

[ExecuteInEditMode]
public class InputMicrocontrollerMessager : Messager
{
    protected override void OnMessageReceived(string message)
    {
        RawChairStatus status = ChairMessageParser.ParseMessage(message);
        Events.Instance.Raise(new InputUpdate());
    }
}

public class InputUpdate : Event
{
}