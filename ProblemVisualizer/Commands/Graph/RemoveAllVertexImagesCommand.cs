using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class RemoveAllVertexImageCommand()
    : ProblemVisualizerCommand<GraphData>
{
    public override void Execute(ref GraphData data)
    {
        foreach (var vertex in data.Vertices)
        {
            vertex.VertexImage = null;
        }
    }
}