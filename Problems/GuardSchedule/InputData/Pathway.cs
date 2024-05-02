namespace Problem.GuardSchedule;

public record Pathway(List<int> Vertices, int MaxPossibleSteps)
{

    public List<int> Vertices { get; set; } = Vertices;
}