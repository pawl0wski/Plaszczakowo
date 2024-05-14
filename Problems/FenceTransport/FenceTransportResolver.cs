using Drawer.GraphDrawer;
using Problem.FenceTransport;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.FenceTransport;

public class FenceTransportResolver : ProblemResolver<FenceTransportInputData, FenceTransportOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    public override FenceTransportOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        FenceTransportOutput output = new();
        problemRecreationCommands = commands;
        return output;
    }
}

