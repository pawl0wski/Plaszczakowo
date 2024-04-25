namespace Problem.Demo;
using ProblemVisualizer;
using GraphDrawer;

public class FirstDemoSnapshotCreator : FirstSnapshotCreator<DemoInputData, GraphData>
{
    public override GraphData CreateFirstSnapshot(DemoInputData inputData)
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];
        var x = 20;
        const int y = 100;

        for (var i = 0; i < inputData.Edges; i++)
        {
            vertices.Add(new GraphVertex(x, y));
            x += 50;
        }

        for (var i = 1; i < vertices.Count; i++)
        {
            edges.Add(new GraphEdge(vertices[i-1], vertices[i]));
        }

        return new GraphData(vertices, edges);
    }
}