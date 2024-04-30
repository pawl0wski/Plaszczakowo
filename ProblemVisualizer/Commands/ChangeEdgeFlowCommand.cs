namespace ProblemVisualizer.Commands;

using Drawer.GraphDrawer;

public class ChangeEdgeFlowCommand(int id, GraphFlow flow)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly GraphFlow Flow = flow;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeFlow(Id, Flow);
    }
}