using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ClearGraphCommand()
    : ProblemVisualizerCommand<GraphData>
{

    public override void Execute(ref GraphData data)
    {
        data.Vertices.Clear();
        data.Edges.Clear();
        data.Texts.Clear();
    }
}