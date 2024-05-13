using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.FenceTransport;

public class ConvexHullFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];

        CreateVertices(vertices);
        
        return new (vertices, [], []);
    }

    private void CreateVertices(List<GraphVertex> vertices)
    {
        inputData.Vertices.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var vertex in inputData.Vertices) vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0));
    }
}