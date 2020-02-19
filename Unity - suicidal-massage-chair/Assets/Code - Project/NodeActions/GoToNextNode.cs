using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;
using Event = Framework.Event;

public class GoToNextNode : NodeAction
{
    public Node NodeToGoTo;

    public override void Invoke(Event e)
    {
        Debug.Log($"Lets go to Node {NodeToGoTo.name}");
    }
}