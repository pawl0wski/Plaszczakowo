using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.FenceTransport;

public class ConvexHullFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private readonly FenceTransportInputData _inputData = inputData;

    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];

        CreateVertices(vertices);

        return new GraphData(vertices, [], []);
    }

    private void CreateVertices(List<GraphVertex> vertices)
    {
        _inputData.Vertices.Sort((x, y) => x.Id.CompareTo(y.Id));
        foreach (var vertex in _inputData.Vertices) vertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0));
    }
}