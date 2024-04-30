using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeEdgeStateCommand(int id, GraphState state)
    : ProblemVisualizerCommand<GraphData>
{
    private readonly int Id = id;
    private readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeState(Id, State);
    }
}