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
        _fence = outputData.ToGraphData(inputData);
    }
}