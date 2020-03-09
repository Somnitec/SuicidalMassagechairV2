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
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;


public class GoBackNode : BaseNode
{
    public override void OnNodeEnable()
    {
        NodeGraph.PlayGoBackNode();
    }

    protected override bool HasConnections()
    {
        var hasConnection = NodeGraph.HasGoBackNode;
        
        if(!hasConnection)
            Debug.LogWarning("No Connections for GoBackNode");
        
        return hasConnection;
    }
}

public class GoBackConsumer : BaseNode
{
    public Connection Output;
    private NodePort outputPort => GetPort("Output");

    public override void OnNodeEnable()
    {
        var nodeGraph = (NodeGraph)graph;
        nodeGraph.ResetGoBackNode();
        GoToNode(outputPort);
    }

    protected override bool HasConnections()
    {
        return outputPort.IsConnected;
    }
}

