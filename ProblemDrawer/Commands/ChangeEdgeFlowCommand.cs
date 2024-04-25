using GraphDrawer;

namespace ProblemDrawer.Commands;

public class ChangeEdgeFlowCommand(int id, GraphFlow flow)
    : ProblemDrawerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly GraphFlow Flow = flow;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeFlow(Id, Flow);
    }
}