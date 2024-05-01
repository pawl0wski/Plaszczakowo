namespace Problem.GuardSchedule;

public record Pathway(List<int> Vertices, int MaxPossibleSteps)
{
    public int MaxPossibleSteps { get; set; } = MaxPossibleSteps;
    public int MaxVertexValue { get; set; } = Vertices.Max();
    public List<int> Vertices { get; set; } = Vertices;
}