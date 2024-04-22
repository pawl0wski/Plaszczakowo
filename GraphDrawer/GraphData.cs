namespace GraphDrawer;

public class GraphData
{
    public List<GraphVertex> Vertices;
    public List<GraphEdge> Edges;

    public GraphData(List<GraphVertex> vertices, List<GraphEdge> edges) {
        Vertices = vertices;
        Edges = edges;
    }

    public void ChangeEdgeStatus(int index, GraphState state)
    {
        var currentEdge = Edges[index];
        currentEdge.State = state;
    }
    
    public void ChangeVertexStatus(int index, GraphState state)
    {
        var currentVertex = Vertices[index];
        currentVertex.State = state;
    }
}