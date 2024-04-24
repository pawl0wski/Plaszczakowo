using GraphDrawer;

namespace ProblemDrawer.Commands;

public class ChangeEdgeFlowCommand(int edgeId, GraphFlow flow)
    : ProblemDrawerCommand<GraphData>
{
    private readonly int _edgeId = edgeId;
    private readonly GraphFlow _flow = flow;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeFlow(_edgeId, _flow);
    }
}