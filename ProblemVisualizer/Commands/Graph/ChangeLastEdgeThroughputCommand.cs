using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeLastEdgeThroughputCommand(GraphThroughput throughtput)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly GraphThroughput Throughtput = throughtput;

    public override void Execute(ref GraphData data)
    {
        data.Edges.Last().Throughput = Throughtput;
    }
}