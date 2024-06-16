
using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.Problems.FenceTransport.Output;

namespace Plaszczakowo.States;

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
        return _fence;
    }

    public bool IsFenceSet()
        => _fence != null;

    public void SetFence(FenceTransportInputData inputData, ConvexHullOutput outputData)
    {
        _fence = outputData.ToGraphData(inputData);
    }
}