using ProblemResolver;
using ProblemResolver.Graph;

namespace Problem.Demo;

public record DemoInputData(List<ProblemVertex> Vertices, List<ProblemEdge> Edges) : ProblemGraphInputData(Vertices, Edges)
{
}