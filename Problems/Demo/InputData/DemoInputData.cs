using GraphDrawer;
using ProblemResolver.Graph;

namespace Problem.Demo;
using ProblemResolver;

public record DemoInputData(List<ProblemVertex> vertices, List<ProblemEdge> edges) : ProblemInputData
{
    public List<ProblemVertex> Vertices = vertices;

    public List<ProblemEdge> Edges = edges;
}