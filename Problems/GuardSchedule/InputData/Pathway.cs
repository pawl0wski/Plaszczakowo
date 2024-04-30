namespace ProblemResolver.GuardSchedule;

public class Pathway
{
    public int MaxPossibleSteps;
    public int MaxVertexValue;
    public List<int> Vertices;

    public Pathway(List<int> vertices, int maxPossibleSteps)
    {
        Vertices = vertices;
        MaxVertexValue = Vertices.Max();
        MaxPossibleSteps = maxPossibleSteps;
    }
}