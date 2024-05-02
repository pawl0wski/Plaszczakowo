using ProblemResolver.Graph;

namespace ProblemResolver;

public record ProblemGraphInputData(List<ProblemVertex> Vertices, List<ProblemEdge> Edges) : ProblemInputData
{
    public List<ProblemVertex> Vertices { get; set; } = Vertices;

    public List<ProblemEdge> Edges { get; set; } = Edges;
}
 
