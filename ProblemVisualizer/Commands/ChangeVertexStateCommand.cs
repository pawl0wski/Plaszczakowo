namespace ProblemVisualizer.Commands;

using GraphDrawer;

public class ChangeVertexStateCommand(int id, GraphState state) : ProblemVisualizerCommand<GraphData>
{
    private readonly int Id = id;
    private readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        data.ChangeVertexStatus(Id, State);
    }
}