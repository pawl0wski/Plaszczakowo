namespace Drawer.GraphDrawer;

public class GraphData : DrawerData
{
    public List<GraphEdge> Edges;
    public List<GraphVertex> Vertices;

    public GraphData(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }
    
    public GraphData()
    {
        Vertices = [];
        Edges = [];
    }

    public override object Clone()
    {
        return new GraphData(
            Vertices.Select(vertex => (GraphVertex)vertex.Clone()).ToList(),
            Edges.Select(edge => (GraphEdge)edge.Clone()).ToList()
        );
    }

    public void ChangeEdgeState(int index, GraphState state)
    {
        var currentEdge = Edges[index];
        currentEdge.State = state;
    }

    public void ChangeEdgeFlow(int index, GraphFlow flow)
    {
        var currentEdge = Edges[index];
        currentEdge.Flow = flow;
    }

    public void ChangeVertexStatus(int index, GraphState state)
    {
        var currentVertex = Vertices[index];
        currentVertex.State = state;
    }

    public void AddVertex(GraphVertex vertex)
    {
        Vertices.Add(vertex);
    }

    public void AddEdge(GraphEdge edge)
    {
        Edges.Add(edge);
    }

    public void DeleteVertexAndAssociatedEdges(GraphVertex vertex)
    {
        Vertices.Remove(vertex);

        Edges.RemoveAll((edge) => edge.From == vertex || edge.To == vertex);
    }
}