namespace Drawer.GraphDrawer;

public class GraphData : DrawerData
{
    public List<GraphEdge> Edges;
    public List<GraphVertex> Vertices;
    public List<GraphText> Texts;

    public GraphData(List<GraphVertex> vertices, List<GraphEdge> edges, List<GraphText> texts)
    {
        Vertices = vertices;
        Edges = edges;
        Texts = texts;
    }

    public GraphData()
    {
        Vertices = [];
        Edges = [];
        Texts = [];
    }

    public override object Clone()
    {
        return new GraphData(
            Vertices.Select(vertex => (GraphVertex)vertex.Clone()).ToList(),
            Edges.Select(edge => (GraphEdge)edge.Clone()).ToList(),
            Texts.Select(text => (GraphText)text.Clone()).ToList()
        );
    }

    public void FillImagesWithProvider(IGraphVertexImageProvider provider)
    {
        foreach (var vertex in Vertices.Where(v => v.VertexImage is not null))
        {
            vertex.FillImageWithProvider(provider);
        }
    }
    public void ChangeEdgeState(int index, GraphState state)
    {
        var currentEdge = Edges[index];
        currentEdge.State = state;
    }

    public void ChangeEdgeFlow(int index, GraphThroughput throughput)
    {
        var currentEdge = Edges[index];
        currentEdge.Throughput = throughput;
    }

    public void ChangeVertexStatus(int index, GraphState state)
    {
        var currentVertex = Vertices[index];
        currentVertex.State = state;
    }

    public void ChangeVertexText(int index, string value)
    {
        var currentVertex = Vertices[index];
        currentVertex.Value = value;
    }

    public void ChangeVertexImage(int index, GraphVertexImage image)
    {
        var currentVertex = Vertices[index];
        currentVertex.VertexImage = image;
    }
    
    public void RemoveVertexImage(int index)
    {
        var currentVertex = Vertices[index];
        currentVertex.VertexImage = null;
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
    public void RemoveEdge(int edgeIndex)
    {
        Edges.RemoveAt(edgeIndex);
    }
}