using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class AddNewEdgeCommand(int sourceId, int destinationId)
    : ProblemVisualizerCommand<GraphData>
{

    public override void Execute(ref GraphData data)
    {
        if (sourceId > data.Vertices.Count || destinationId > data.Vertices.Count) 
        {
            return;
        }
        GraphEdge edge = new(data.Vertices[sourceId], data.Vertices[destinationId]);
        data.Edges.Add(edge);
    }
}