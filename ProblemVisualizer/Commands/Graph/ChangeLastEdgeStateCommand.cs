using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeLastEdgeStateCommand(GraphState state)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        data.Edges.Last().State = State;
    }
}