namespace Problem.CarrierAssignment;

public class CarrierAssignmentOutputStep : ProblemOutputStep
{
    public List<int> Vertices;
    public List<Edge> Edges;

    public CarrierAssignmentOutputStep(List<int> vertices, List<Edge> edges) {
        this.Vertices = vertices;
        this.Edges = edges;
    }
}