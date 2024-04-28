namespace ProblemResolver.Graph;

public record ProblemEdge(int Id, int From, int To)
{
    public readonly int Id = Id;
    
    public readonly int From = From;
    public readonly int To = To;
}