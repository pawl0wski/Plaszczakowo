using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class AddNewVertexCommand(int X, int Y, char character, GraphState? state)
    : ProblemVisualizerCommand<GraphData>
{

    public override void Execute(ref GraphData data)
    {
        data.Vertices.Add(new(X, Y, character.ToString(), state ?? GraphStates.Inactive));
    }
}