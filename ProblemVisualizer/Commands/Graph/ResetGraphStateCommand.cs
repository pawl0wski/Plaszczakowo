using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ResetGraphStateCommand : ProblemVisualizerCommand<GraphData>
{
    public override void Execute(ref GraphData data)
    {
        foreach (var vertex in data.Vertices) vertex.State = GraphStates.Inactive;

        foreach (var edge in data.Edges) edge.State = GraphStates.Inactive;
    }
}