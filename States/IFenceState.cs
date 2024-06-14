using Drawer.GraphDrawer;
using Problem.FenceTransport;

namespace ProjektZaliczeniowy_AiSD2.States;

public interface IFenceState
{
    public void SetFence(FenceTransportInputData inputData, ConvexHullOutput outputData);
    public GraphData GetFence();
    public bool IsFenceSet();

    public void ClearFence();
}