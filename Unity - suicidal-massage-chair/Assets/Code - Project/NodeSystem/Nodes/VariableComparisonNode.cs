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
        return TruePort.IsConnected || FalsePort.IsConnected;
    }

    [Button]
    protected override void Init()
    {
        Comparison.Init(bb);
    }
}

