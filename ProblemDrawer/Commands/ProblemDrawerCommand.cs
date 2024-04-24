using GraphDrawer;

namespace ProblemDrawer;

public abstract class ProblemDrawerCommand<TDrawerData>
    where TDrawerData : GraphData
{
    public abstract void Execute(ref TDrawerData data);
}