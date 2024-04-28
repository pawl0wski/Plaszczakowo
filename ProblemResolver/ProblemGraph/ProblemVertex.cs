namespace ProblemResolver.Graph;

public record ProblemVertex(int Id, int? X, int? Y)
{
    public readonly int Id = Id;

    public readonly int? X = X;

    public readonly int? Y = Y;
}