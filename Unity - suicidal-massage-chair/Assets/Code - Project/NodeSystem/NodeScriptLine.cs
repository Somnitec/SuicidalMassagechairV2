using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using static System.String;

[HideReferenceObjectPicker]
public struct NodeScriptLine
{
    [TableColumnWidth(55, false), Min(0)] public float TimeSec;
    [InlineProperty]
    public NodeScriptFunction Function;
}

public class ScriptEvent : GenericEvents<NodeScriptFunction>
{
}

public abstract class NodeScriptFunction
{
    protected int True => 1;
    protected int False => 0;

    public virtual void RaiseEvent()
    {
        ScriptEvent.Instance.Raise(this);
    }

    public abstract List<string> SerializeToJson();

    public string ToJson(string param, params int[] values)
    {
        return MessageHelper.ToJson(param, values);
    }

    public string SerializeToString()
    {
        return Join(",", SerializeToJson());
    }

    protected int BoolToString(bool b)
    {
        return b ? True : False;
    }

    protected List<string> ToList(params string[] s)
    {
        return s.ToList();
    }
}