using GraphDrawer;
using Problem;

namespace ProblemVisualizer;

abstract class ProblemVisualizerCommandsExecutor<TDrawerData> where TDrawerData : ICloneable
{
    private ProblemVisualizerSnapshots<TDrawerData> _snapshots;

    private List<ProblemVisualizerCommands<TDrawerData>> _listOfCommands;
    
    public abstract void CreateFirstSnapshot(in ProblemInputData inputData);

    public ProblemVisualizerCommandsExecutor(
        ProblemVisualizerSnapshots<TDrawerData> snapshots,
        List<ProblemVisualizerCommands<TDrawerData>> listOfCommands)
    {
        _snapshots = snapshots;
        _listOfCommands = listOfCommands;
    }
    
    public void ExecuteCommands()
    {
        foreach (var commands in _listOfCommands)
        {
            ExecuteAllCommands(commands);
        }
    }
    private void ExecuteAllCommands(ProblemVisualizerCommands<TDrawerData> commands)
    {
        var drawerData = CreateCloneOfLastDrawerData();
        while (commands.Count > 0)
        {
            commands.Dequeue().Execute(ref drawerData);
        }
        _snapshots.Add(drawerData);
    }
    private TDrawerData CreateCloneOfLastDrawerData()
    {
        return (TDrawerData)_snapshots.Last().Clone();
    }
    
}