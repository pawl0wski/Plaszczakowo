using GraphDrawer;
using ProblemDrawer;

namespace ChangeVertexStatusCommand;

public class ChangeVertexStateCommand(int id, GraphState state) : ProblemDrawerCommand<GraphData>
{
    private readonly int Id = id;
    private readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        data.ChangeVertexStatus(Id, State);
    }
}