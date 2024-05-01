using ProblemResolver.Graph;

namespace ProblemResolver;

public record ProblemGraphInputData(List<ProblemVertex> Vertices, List<ProblemEdge> Edges) : ProblemInputData
{
    public readonly List<ProblemVertex> Vertices = Vertices;
    
    public readonly List<ProblemEdge> Edges = Edges;
}
 
