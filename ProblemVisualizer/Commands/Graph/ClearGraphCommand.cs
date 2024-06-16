using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

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