using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;


public class DialogueNode : BaseNode
{
    public String Name;
    public NodeData Data; // add different languages later
    [Output(dynamicPortList = true)]
    public List<Condition> Ports;

    [Output(dynamicPortList = true)]
    public List<UserInputButton> Condition;
}

public class BaseNode : XNode.Node
{
    [Input] public Connection Input;
}

[Serializable]
public class Connection
{
}

public abstract class Condition
{
    // Add a method?
}

public class UserInput : Condition
{
    public UserInputButton Button;
}