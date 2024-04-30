using Drawer.GraphDrawer;
using ProblemResolver.Graph;

namespace Problem.Demo;
using ProblemResolver;

public record DemoInputData(List<ProblemVertex> Vertices, List<ProblemEdge> Edges) : ProblemInputData
{
    public List<ProblemVertex> Vertices { get; set; } = Vertices;

    public List<ProblemEdge> Edges { get; set; } = Edges;
}