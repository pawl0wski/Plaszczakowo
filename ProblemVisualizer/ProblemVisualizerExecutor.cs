using GraphDrawer;
using ProblemResolver;

namespace ProblemVisualizer;

public class ProblemVisualizerExecutor<TInputData, TDrawerData> 
    where TInputData : ProblemInputData
    where TDrawerData : ICloneable
{
    private ProblemVisualizerSnapshots<TDrawerData> _snapshots;

    private ProblemVisualizerCommandsQueue<TDrawerData>[] _listOfCommands;

    private FirstSnapshotCreator<TInputData, TDrawerData> _firstSnapshotCreator;

    public ProblemVisualizerExecutor(
        ProblemVisualizerSnapshots<TDrawerData> snapshots,
        ProblemVisualizerCommandsQueue<TDrawerData>[] listOfCommands,
        FirstSnapshotCreator<TInputData, TDrawerData> firstSnapshotCreator)
    {
        _snapshots = snapshots;
        _listOfCommands = listOfCommands;
        _firstSnapshotCreator = firstSnapshotCreator;
    }

    public void CreateFirstSnapshot(TInputData inputData)
    {
        _snapshots.Add(_firstSnapshotCreator.CreateFirstSnapshot(inputData));
    }
    public void ExecuteCommands()
    {
        foreach (var commands in _listOfCommands)
        {
            ExecuteAllCommands(commands);
        }
    }
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