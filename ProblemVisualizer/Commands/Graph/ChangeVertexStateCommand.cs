using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeVertexStateCommand(int id, GraphState state)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        data.ChangeVertexStatus(Id, State);
    }
}