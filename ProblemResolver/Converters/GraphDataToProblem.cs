using Drawer.GraphDrawer;
using ProblemResolver.Graph;

namespace ProblemResolver.Converters;

public static class GraphDataToProblem
{
    public static ProblemGraphInputData Convert(GraphData data)
    {
        List<ProblemVertex> vertices = [];
        List<ProblemEdge> edges = [];

        for (var i = 0; i < data.Vertices.Count; i++)
        {
            var currentVertex = data.Vertices[i];
            vertices.Add(new ProblemVertex(i, currentVertex.X, currentVertex.Y));
        }

        for (var i = 0; i < data.Edges.Count; i++)
        {
            var currentEdge = data.Edges[i];
            edges.Add(new ProblemEdge(i,
                data.Vertices.IndexOf(currentEdge.From),
                data.Vertices.IndexOf(currentEdge.To)
            ));
        }

        return new(vertices, edges); 
    }
}

