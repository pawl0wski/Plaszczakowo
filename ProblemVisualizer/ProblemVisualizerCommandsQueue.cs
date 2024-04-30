using Drawer;

namespace ProblemVisualizer;

public class ProblemVisualizerCommandsQueue<TDrawerData>
    : Queue<ProblemVisualizerCommand<TDrawerData>>
    where TDrawerData : DrawerData
{
}