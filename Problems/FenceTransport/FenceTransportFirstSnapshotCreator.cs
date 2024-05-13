using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.FenceTransport;

public class FenceTransportFirstSnapshotCreator(FinalFenceInputData inputData)
    : FirstSnapshotCreator<FinalFenceInputData, GraphData>(inputData)
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

    private void CreateVertices(List<GraphVertex> vertices)
    {
        inputData.InputData.Vertices.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var vertex in inputData.InputData.Vertices) {
            if (vertex.Id == InputData.InputData.FactoryIndex)
                vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, "Fabryka", GraphStates.Special));
            else if (InputData.ConvexHullOutput.HullIndexes!.Contains(vertex.Id))
                vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, null, GraphStates.Active));
            else
                vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0));
        }
    }
    private void CreateEdges(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        foreach (var edge in inputData.InputData.Edges) edges.Add(new GraphEdge(vertices[edge.From], vertices[edge.To]));
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