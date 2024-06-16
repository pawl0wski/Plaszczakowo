using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.Problems.FenceTransport.Output;

namespace Plaszczakowo.States;

public interface IFenceState
{
    public void SetFence(FenceTransportInputData inputData, ConvexHullOutput outputData);
    public GraphData GetFence();
    public bool IsFenceSet();

    public void ClearFence();
}