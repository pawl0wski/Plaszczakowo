using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.Demo;

public class FirstDemoSnapshotCreator(DemoInputData inputData)
    : FirstSnapshotCreator<DemoInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];

        foreach (var vertex in inputData.Vertices) vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0));

        foreach (var edge in inputData.Edges) edges.Add(new GraphEdge(vertices[edge.From], vertices[edge.To]));

        return new (vertices, edges, []);
    }
}