using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeEdgeFlowCommand(int id, GraphThroughput flow)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly GraphThroughput Flow = flow;
    public readonly int Id = id;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeFlow(Id, Flow);
    }
}