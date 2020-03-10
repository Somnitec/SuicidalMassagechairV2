using Sirenix.OdinInspector;
using UnityEngine;

public class GoBackNode : BaseNode
{
    [InfoBox("Skip the audio and go straight to input mode")]
    public bool GoBackInWaitMode = false;
    
    public override void OnNodeEnable()
    {
        NodeGraph.PlayGoBackNode(GoBackInWaitMode);
    }

    protected override bool HasConnections()
    {
        var hasConnection = NodeGraph.HasGoBackNode;
        
        if(!hasConnection)
            Debug.LogWarning("No Connections for GoBackNode");
        
        return hasConnection;
    }
}