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
using UnityEngine;
using XNode;


public class VariableComparisonNode : BaseNode
{
    public Comparison Comparison = new Comparison();

    [Output()] public Connection True;
    [Output()] public Connection False;

    private NodePort TruePort => GetOutputPort("True");
    private NodePort FalsePort => GetOutputPort("False");
    private BlackBoard bb => NodeGraph.BlackBoard;

    public override void OnNodeEnable()
    {
        if (Comparison.Compare())
            GoToNode(TruePort);
        else
            GoToNode(FalsePort);
    }

    protected override bool HasConnections()
    {
        var hasConnections = TruePort.IsConnected || FalsePort.IsConnected;
        if(!hasConnections)
            Debug.LogWarning("VariableModifyNode Has no connections");
        
        return hasConnections;
    }

    [Button]
    protected override void Init()
    {
        Comparison.Init(bb);
    }
}

