using GraphDrawer;
using ProblemResolver;

namespace ProblemVisualizer;

public class ProblemVisualizerExecutor<TInputData, TDrawerData> 
    where TInputData : ProblemInputData
    where TDrawerData : ICloneable
{
    private ProblemVisualizerSnapshots<TDrawerData> _snapshots = new();

    private List<ProblemVisualizerCommandsQueue<TDrawerData>> _listOfCommands;

    private FirstSnapshotCreator<TInputData, TDrawerData> _firstSnapshotCreator;

    public ProblemVisualizerExecutor(
        List<ProblemVisualizerCommandsQueue<TDrawerData>> listOfCommands,
        FirstSnapshotCreator<TInputData, TDrawerData> firstSnapshotCreator)
    {
        _listOfCommands = listOfCommands;
        _firstSnapshotCreator = firstSnapshotCreator;
    }

    public void CreateFirstSnapshot()
    {
        _snapshots.Add((TDrawerData)_firstSnapshotCreator.CreateFirstSnapshot());
    }
    
    public void ExecuteCommands()
    {
        foreach (var commands in _listOfCommands)
        {
            ExecuteAllCommands(commands);
        }
    }

    public ProblemVisualizerSnapshots<TDrawerData> GetSnapshots() => _snapshots;
    private void ExecuteAllCommands(ProblemVisualizerCommandsQueue<TDrawerData> commandsQueue)
    {
        var drawerData = CreateCloneOfLastDrawerData();
        while (commandsQueue.Count > 0)
        {
            commandsQueue.Dequeue().Execute(ref drawerData);
        }
        _snapshots.Add(drawerData);
    }
    private TDrawerData CreateCloneOfLastDrawerData()
    {
        return (TDrawerData)_snapshots.Last().Clone();
    }
    
}