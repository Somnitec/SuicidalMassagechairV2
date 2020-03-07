using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using XNode;


public class GoBackNode : BaseNode
{
    public override void OnNodeEnable()
    {
        NodeGraph.PlayGoBackNode();
    }

    protected override bool HasConnections()
    {
        return NodeGraph.HasGoBackNode;
    }
}

