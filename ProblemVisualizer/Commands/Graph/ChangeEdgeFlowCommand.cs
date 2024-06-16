using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeEdgeFlowCommand(int id, GraphThroughput throughput)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly GraphThroughput Throughput = throughput;
    public readonly int Id = id;

    public override void Execute(ref GraphData data)
    {
        if (Id > data.Edges.Count || Id < 0) 
        {
            return;
        }
        data.ChangeEdgeFlow(Id, Throughput);
    }
}