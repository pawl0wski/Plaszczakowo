namespace ProblemDrawer.Commands;

using GraphDrawer;

public class ChangeEdgeStateCommand(int edgeId, GraphState graphState) : ProblemDrawerCommand<GraphData>
{
    private readonly int _edgeId = edgeId;
    private readonly GraphState _graphState = graphState;

    public override void Execute(ref GraphData data)
    {
        data.ChangeEdgeState(_edgeId, _graphState);
    }
}

