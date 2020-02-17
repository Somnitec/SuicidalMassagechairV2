using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

[System.Serializable]
public class NodeScriptLine
{
    public float Time;
    public NodeScriptFunction function;
}

public class ScriptEvent : GenericEvents<NodeScriptFunction>
{
}

public abstract class NodeScriptFunction : ScriptableObject
{
}