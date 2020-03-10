using UnityEngine;

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