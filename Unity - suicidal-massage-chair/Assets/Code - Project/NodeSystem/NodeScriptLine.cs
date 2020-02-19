using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class NodeScriptLine
{
    [TableColumnWidth(100,false), Min(0)]
    public float TimeInSec;
    public NodeScriptFunction Function;
}

public class ScriptEvent : GenericEvents<NodeScriptFunction>
{
}

public abstract class NodeScriptFunction
{
    public virtual void RaiseEvent()
    {
        ScriptEvent.Instance.Raise(this);
    }
}