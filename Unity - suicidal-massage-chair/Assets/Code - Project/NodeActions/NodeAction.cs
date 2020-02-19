using UnityEngine;
using Event = Framework.Event;

public abstract class NodeAction
{
    public abstract void Invoke(Event e);
}