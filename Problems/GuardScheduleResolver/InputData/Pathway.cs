namespace ProblemResolver.GuardSchedule;
public class Pathway
{
    public List<int> Vertices;
    public int MaxVertexValue;
    public int MaxPossibleSteps;

    public Pathway(List<int> vertices, int maxPossibleSteps)
    {
        this.Vertices = vertices;
        this.MaxVertexValue = this.Vertices.Max();
        this.MaxPossibleSteps = maxPossibleSteps;
    }
}
