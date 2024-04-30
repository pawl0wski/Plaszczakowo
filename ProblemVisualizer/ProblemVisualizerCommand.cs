namespace ProblemVisualizer;

public abstract class ProblemVisualizerCommand<TDrawerData>
    where TDrawerData : ICloneable
{
    public abstract void Execute(ref TDrawerData data);
}