namespace ProblemResolver.Graph;

public record ProblemVertex(int Id, int? X, int? Y)
{
    public int Id { get; set; } = Id;

    public int? X { get; set; } = X;

    public int? Y { get; set; } = Y;
}