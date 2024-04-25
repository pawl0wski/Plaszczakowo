namespace Problem.Demo;
using ProblemResolver;

public record DemoInputData : ProblemInputData
{
    public int Edges;

    public DemoInputData(int edges)
    {
        Edges = edges;
    }
}