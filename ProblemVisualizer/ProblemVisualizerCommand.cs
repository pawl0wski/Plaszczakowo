using Plaszczakowo.Drawer;

namespace Plaszczakowo.ProblemVisualizer;

public abstract class ProblemVisualizerCommand<TDrawerData>
    where TDrawerData : DrawerData
{
    public abstract void Execute(ref TDrawerData data);
}