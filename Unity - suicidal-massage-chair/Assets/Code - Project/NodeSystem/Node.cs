using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using Event = Framework.Event;

[CreateAssetMenu, InlineEditor(InlineEditorModes.FullEditor)]
public class Node : SerializedScriptableObject
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
        ActionsAfterDataPlay.ForEach(ea => { Events.Instance.AddListener(ea.Condition, ea.Action.Invoke); });
    }

    public void Disable()
    {
        Debug.Log($"Disabled {Name} remove all ");

        Data.OnFinished -= OnDataFinished;
        ActionsAfterDataPlay.ForEach(ea =>
        {
            Events.Instance.RemoveListener(ea.Condition, ea.Action.Invoke);
        });
    }
}

public class NodeEventAction
{
    [TypeFilter("GetFilteredTypeList")]
    [InlineProperty]
    public EventAction Condition;
    [InlineProperty]
    public NodeAction Action;

    public IEnumerable<Type> GetFilteredTypeList()
    {
        var q = typeof(EventAction).Assembly.GetTypes()
            .Where(x => !x.IsAbstract) // Excludes BaseClass
            .Where(x => !x.IsGenericTypeDefinition)
            .Where(x =>typeof(EventAction).IsAssignableFrom(x)); 

        return q;
    }
}

public abstract class EventAction : Event
{
}

// TODO rethink if pressed, down and up is the way to go
public class UserInputPressed : EventAction
{
    public UserInputButton Button;
}

public class UserInputDown : EventAction
{
    public UserInputButton Button;
}

public class UserInputUp : EventAction
{
    public UserInputButton Button;

    public UserInputUp(UserInputButton button)
    {
        Button = button;
    }
}