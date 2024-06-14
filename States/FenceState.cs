using Drawer.GraphDrawer;
using Problem.FenceTransport;

namespace ProjektZaliczeniowy_AiSD2.States;

public class FenceState : IFenceState
{
    private GraphData? _fence = null;

    public void ClearFence()
    {
        _fence = null;
    }

    public GraphData GetFence(){
        if (_fence == null)
            throw new InvalidOperationException("Fence is not set");
        return (GraphData)_fence.Clone();
    }

    public bool IsFenceSet()
        => _fence != null;

    public void SetFence(FenceTransportInputData inputData, ConvexHullOutput outputData)
    {
        GraphData graphData = new();
        
        if (outputData.HullIndexes == null)
            throw new ArgumentNullException(nameof(outputData.HullIndexes));
        
        foreach (var vertexId in outputData.HullIndexes)
        {
            graphData.Vertices.Add(inputData.Vertices.First(v => v.Id == inputData.Vertices[vertexId].Id).ToGraphVertex());
        }

        for (int i = 1; i < graphData.Vertices.Count(); i++)
        {
            graphData.AddEdge(new GraphEdge(graphData.Vertices[i-1], graphData.Vertices[i], directed: true));
        }
        graphData.AddEdge(new GraphEdge(graphData.Vertices.Last(), graphData.Vertices.First(), directed: true));

        _fence = graphData;
    }
}