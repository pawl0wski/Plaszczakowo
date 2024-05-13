using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class ConvexHullFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];

        CreateLandmarks(vertices);
        
        return new (vertices, [], []);
    }

    private void CreateLandmarks(List<GraphVertex> vertices)
    {
        inputData.Landmarks.Sort((x, y)=>x.Id.CompareTo(y.Id));
        foreach (var landmark in inputData.Landmarks) vertices.Add(new GraphVertex(landmark.X ?? 0, landmark.Y ?? 0));
    }
}