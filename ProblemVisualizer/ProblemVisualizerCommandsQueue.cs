using Plaszczakowo.Drawer;

namespace Plaszczakowo.ProblemVisualizer;

public class ProblemVisualizerCommandsQueue<TDrawerData>
    : Queue<ProblemVisualizerCommand<TDrawerData>>
    where TDrawerData : DrawerData;