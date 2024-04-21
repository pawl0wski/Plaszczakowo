public class GraphInput
{
    public List<GraphVertex> Vertices;
    public List<GraphEdge> Edges;

    public GraphInput(List<GraphVertex> vertices, List<GraphEdge> edges) {
        this.Vertices = vertices;
        this.Edges = edges;
    }
}