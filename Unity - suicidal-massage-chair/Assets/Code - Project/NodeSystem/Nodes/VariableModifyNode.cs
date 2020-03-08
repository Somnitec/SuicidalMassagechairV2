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


public class VariableModifyNode : BaseNode
{
    public Modifier Modifier = new Modifier();

    [Output()] public Connection Output;

    private NodePort OutputPort => GetOutputPort("Output");
    private BlackBoard bb => NodeGraph.BlackBoard;

    public override void OnNodeEnable()
    {
        Modifier.Modify();
        GoToNode(OutputPort);
    }

    protected override bool HasConnections()
    {
        var hasConnection = OutputPort.IsConnected;
        if(!hasConnection)
            Debug.LogWarning("VariableModifyNode Has no connections");
        return hasConnection;
    }

    [Button]
    protected override void Init()
    {
        Modifier.Init(bb);
    }
}