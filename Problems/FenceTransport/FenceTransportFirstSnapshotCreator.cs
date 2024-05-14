using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.FenceTransport;

public class FenceTransportFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];

        CreateVertices(vertices);
        CreateEdges(vertices, edges);
        CreateFence(vertices, edges);
        return new (vertices, edges, []);
    }

    private void CreateVertices(List<GraphVertex> graphVertices)
    {
        if (inputData == null || inputData == null)
        {
            throw new ArgumentNullException(nameof(inputData), "Input data cannot be null");
        }

        inputData.Vertices.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var vertex in inputData.Vertices) 
        {
            if (vertex == null)
            {
                continue;
            }

            if (vertex.Id == inputData.FactoryIndex)
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, "Fabryka", GraphStates.Special));
            else if (inputData.ConvexHullOutput.HullIndexes!.Contains(vertex.Id))
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, null, GraphStates.Active));
            else
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0));
        }
    }
    private void CreateEdges(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        foreach (var edge in inputData.Edges) edges.Add(new GraphEdge(vertices[edge.From], vertices[edge.To]));
    }
    private void CreateFence(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        var count = inputData.ConvexHullOutput.HullIndexes!.Count;
        for (int i = 0; i < count; i++)
        {
            var from = vertices[inputData.ConvexHullOutput.HullIndexes[i]];
            var to = vertices[inputData.ConvexHullOutput.HullIndexes[(i + 1) % count]];
            edges.Add(new GraphEdge(from, to, GraphStates.Active));
        }
    }
}