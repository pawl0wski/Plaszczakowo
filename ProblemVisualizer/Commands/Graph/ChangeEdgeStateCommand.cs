using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeEdgeStateCommand(int id, GraphState state)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        if (Id > data.Edges.Count || Id < 0) return;
        data.ChangeEdgeState(Id, State);
    }
}