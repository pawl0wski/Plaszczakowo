namespace ProblemResolver.Graph;

public record ProblemEdge(int Id, int From, int To, bool Directed = false)
{
    public int Id { get; set; } = Id;

    public int From { get; set; } = From;
    public int To { get; set; } = To;

    public bool Directed { get; set; } = Directed;
}