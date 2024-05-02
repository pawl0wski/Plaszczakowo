using Drawer.GraphDrawer;

namespace ProblemResolver.Converters;

public static class ProblemToGraphData
{
    public static GraphData Convert(ProblemGraphInputData inputData)
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];

        foreach (var problemVertex in inputData.Vertices)
        {
            if (problemVertex.X is null || problemVertex.Y is null)
                throw new NullReferenceException("X or Y in ProblemVertex can't be null");
            vertices.Add(new GraphVertex(problemVertex.X ?? 0, problemVertex.Y ?? 0));
        }

        foreach (var problemEdge in inputData.Edges)
        {
            edges.Add(new GraphEdge(vertices[problemEdge.From], vertices[problemEdge.To]));
        }

        return new (vertices, edges);
    }
}