using UnityEngine;
using Event = Framework.Event;

public abstract class NodeAction : ScriptableObject
{
    public abstract void Invoke(Event e);
}