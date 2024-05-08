using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class AddNewVertexCommand(int X, int Y, char character, GraphState? state)
    : ProblemVisualizerCommand<GraphData>
{

    public override void Execute(ref GraphData data)
    {
        GraphVertex vertex = new(X, Y, character.ToString(), state == null ? state : GraphStates.Inactive);
        data.Vertices.Add(vertex);
    }
}