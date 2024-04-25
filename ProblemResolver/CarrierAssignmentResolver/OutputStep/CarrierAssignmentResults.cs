using GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class CarrierAssignmentResults : ProblemResults
{
    public List<int> Vertices;
    public List<Edge> Edges;

    public CarrierAssignmentResults(List<int> vertices, List<Edge> edges) {
        this.Vertices = vertices;
        this.Edges = edges;
    }
}