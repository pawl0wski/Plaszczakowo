using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class ConvexHullFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private readonly FenceTransportInputData _inputData = inputData;

    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];

        CreateLandmarks(vertices);
        
        return new (vertices, [], []);
    }

    private void CreateLandmarks(List<GraphVertex> vertices)
    {
        _inputData.Landmarks.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var landmark in _inputData.Landmarks) vertices.Add(new GraphVertex(landmark.X ?? 0, landmark.Y ?? 0));
    }
}