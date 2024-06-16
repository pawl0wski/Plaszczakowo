using Plaszczakowo.ProblemResolver.ProblemGraph;

namespace Plaszczakowo.ProblemResolver.ProblemInput;

public record ProblemGraphInputData(List<ProblemVertex> Vertices, List<ProblemEdge> Edges) : ProblemInputData
{
    public List<ProblemVertex> Vertices { get; set; } = Vertices;

    public List<ProblemEdge> Edges { get; set; } = Edges;
}
 
