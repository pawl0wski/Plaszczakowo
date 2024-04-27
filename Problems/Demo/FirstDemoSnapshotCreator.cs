namespace Problem.Demo;
using ProblemVisualizer;
using GraphDrawer;

public class FirstDemoSnapshotCreator(DemoInputData inputData) : FirstSnapshotCreator<DemoInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];
        var x = 100;
        const int y = 200;

        for (var i = 0; i < InputData.Edges+1; i++)
        {
            vertices.Add(new GraphVertex(x, y));
            x += 150;
        }

        for (var i = 1; i < vertices.Count; i++)
        {
            edges.Add(new GraphEdge(vertices[i-1], vertices[i]));
        }

        return new GraphData(vertices, edges);
    }
}