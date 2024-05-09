using ProblemResolver;
using Drawer.GraphDrawer;

namespace Problem.CarrierAssignment;

public record CarrierAssignmentOutput : ProblemOutput
{
    public List<GraphEdge> Pairs = new();
}