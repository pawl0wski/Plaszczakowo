using GraphDrawer;

namespace ProblemVisualizer;

public class ProblemVisualizerCommands<TDrawerData> : Queue<ProblemVisualizerCommand<TDrawerData>>
    where TDrawerData : ICloneable
{
}