using GraphDrawer;
using ProblemDrawer;

namespace ChangeVertexStatusCommand;

public class ChangeVertexStateCommand(int vertexId, GraphState graphState) : ProblemDrawerCommand<GraphData>
{
    private readonly int _vertexId = vertexId;
    private readonly GraphState _graphState = graphState;

    public override void Execute(ref GraphData data)
    {
        data.ChangeVertexStatus(_vertexId, _graphState);
    }
}