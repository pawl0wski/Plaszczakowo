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
        return new (vertices, edges, []);
    }

    private void CreateVertices(List<GraphVertex> graphVertices)
    {
        var carriersCount = InputData.CarrierAssignmentOutput.Pairs.Count();
        inputData.Vertices.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var vertex in inputData.Vertices) 
        {
            if (vertex.Id == inputData.FactoryIndex)
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, carriersCount.ToString(), GraphStates.Special));
            else if (inputData.ConvexHullOutput!.HullIndexes!.Contains(vertex.Id))
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, 0.ToString(), GraphStates.Active));
            else
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, 0.ToString()));
        }
    }
    private void CreateEdges(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        foreach (var edge in inputData.Edges)
        {
            if (edge.Throughput is null)
                edges.Add(new GraphEdge(vertices[edge.From], vertices[edge.To]));
            else
                edges.Add(new GraphEdge(vertices[edge.From],
                vertices[edge.To], GraphStates.Active,
                throughput: new GraphThroughput(0, edge.Throughput.Capacity)));
        }
    }
}