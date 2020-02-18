using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;
using Event = Framework.Event;

[CreateAssetMenu]
public class Node : ScriptableObject
{
    public String Name;
    public NodeData Data; // add different languages later
    public List<NodeEventAction> ActionsAfterDataPlay;

    public void Enable()
    {
        Debug.Log($"Enabled {Name}");
        Data.OnFinished += OnDataFinished;
    }

    public void OnDataFinished()
    {
        Debug.Log($"OnFinished {Name}");
        ActionsAfterDataPlay.ForEach(ea =>
        {
            Events.Instance.AddListener(ea.EventToSubscribeTo, ea.Action.Invoke);
        });
    }

    public void Disable()
    {
        Debug.Log($"Disabled {Name} remove all ");

        Data.OnFinished -= OnDataFinished;
        ActionsAfterDataPlay.ForEach(ea =>
        {
            Events.Instance.RemoveListener(ea.EventToSubscribeTo, ea.Action.Invoke);
        });
    }
}

[Serializable]
public class NodeEventAction
{
    public Event EventToSubscribeTo;
    public NodeAction Action;
}

// TODO rethink if pressed, down and up is the way to go
public class UserInputPressed : Event
{
    public UserInterfaceMicroControllerData.UserInterfaceButtonValue Button;
}

public class UserInputDown : Event
{
    public UserInterfaceMicroControllerData.UserInterfaceButtonValue Button;
}

public class UserInputUp: Event
{
    public UserInterfaceMicroControllerData.UserInterfaceButtonValue Button;
}