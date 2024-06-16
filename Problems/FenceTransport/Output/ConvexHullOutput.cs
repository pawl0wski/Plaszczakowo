using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.Problems.FenceTransport.Input;

namespace Plaszczakowo.Problems.FenceTransport.Output;

public record ConvexHullOutput : ProblemOutput
{
    public List<int>? HullIndexes { get; set; } = new();

    public GraphData ToGraphData(FenceTransportInputData inputData, bool directed = true)
    {
        GraphData graphData = new();

        if (HullIndexes == null)
            throw new NullReferenceException(nameof(HullIndexes));
        
        foreach (var vertexId in HullIndexes)
        {
            graphData.Vertices.Add(inputData.Vertices.First(v => v.Id == inputData.Vertices[vertexId].Id).ToGraphVertex());
        }

        for (var i = 1; i < graphData.Vertices.Count; i++)
        {
            graphData.AddEdge(new GraphEdge(graphData.Vertices[i-1], graphData.Vertices[i], directed: directed));
        }
        graphData.AddEdge(new GraphEdge(graphData.Vertices.Last(), graphData.Vertices.First(), directed: directed));

        return graphData;
    }
    
}

