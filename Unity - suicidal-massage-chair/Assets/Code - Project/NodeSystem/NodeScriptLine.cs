using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

[System.Serializable]
public class NodeScriptLine
{
    public float Time;
    public NodeScriptFunction Function;
}

public class ScriptEvent : GenericEvents<NodeScriptFunction>
{
}

public abstract class NodeScriptFunction : ScriptableObject
{
    public virtual void RaiseEvent()
    {
        Debug.Log($"Raising: {this.GetType()}");
        ScriptEvent.Instance.Raise(this);
    }
}