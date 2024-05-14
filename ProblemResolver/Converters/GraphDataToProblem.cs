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
            vertices.Add(ProblemVertex.FromGraphVertex(i, currentVertex));
        }

        for (var i = 0; i < data.Edges.Count; i++)
        {
            var currentEdge = data.Edges[i];
            edges.Add(new ProblemEdge(i,
                data.Vertices.IndexOf(currentEdge.From),
                data.Vertices.IndexOf(currentEdge.To),
                currentEdge.Throughput is null ? null :
                ProblemGraphThroughput.FromGraphThroughput(currentEdge.Throughput)
            ));
        }

        return new(vertices, edges); 
    }
}

