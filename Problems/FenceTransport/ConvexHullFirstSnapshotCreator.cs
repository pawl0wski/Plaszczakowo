using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class ConvexHullFirstSnapshotCreator(CarrierAssignmentInputData inputData)
    : FirstSnapshotCreator<CarrierAssignmentInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];

        CreateLandmarks(vertices);
        CreatePaths(vertices, edges);
        
        return new (vertices, edges, []);
    }

    private void CreateLandmarks(List<GraphVertex> vertices)
    {
        inputData.Landmarks.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var landmark in inputData.Landmarks) vertices.Add(new GraphVertex(landmark.X ?? 0, landmark.Y ?? 0));
    }
    private void CreatePaths(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        foreach (var path in inputData.Paths) edges.Add(new GraphEdge(vertices[path.From], vertices[path.To]));
    }
}