using XNode;

public class GoBackConsumer : BaseNode
{
    [Output()] public Connection Output;
    private NodePort outputPort => GetPort("Output");

    public override void OnNodeEnable()
    {
        var nodeGraph = (NodeGraph) graph;
        nodeGraph.ResetGoBackNode();
        GoToNode(outputPort);
    }

    protected override bool HasConnections()
    {
        return outputPort.IsConnected;
    }
}