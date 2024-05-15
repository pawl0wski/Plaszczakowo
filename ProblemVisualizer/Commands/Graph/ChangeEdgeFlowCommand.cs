using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeEdgeFlowCommand(int id, GraphThroughput throughput)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly GraphThroughput Throughput = throughput;
    public readonly int Id = id;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeFlow(Id, Throughput);
    }
}