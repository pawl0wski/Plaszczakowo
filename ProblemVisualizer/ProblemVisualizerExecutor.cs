using Drawer;
using ProblemResolver;

namespace ProblemVisualizer;

public class ProblemVisualizerExecutor<TInputData, TDrawerData>
    where TInputData : ProblemInputData
    where TDrawerData : DrawerData
{
    private readonly FirstSnapshotCreator<TInputData, TDrawerData> _firstSnapshotCreator;

    private readonly List<ProblemVisualizerCommandsQueue<TDrawerData>> _listOfCommands;
    private readonly ProblemVisualizerSnapshots<TDrawerData> _snapshots = [];

    public ProblemVisualizerExecutor(
        List<ProblemVisualizerCommandsQueue<TDrawerData>> listOfCommands,
        FirstSnapshotCreator<TInputData, TDrawerData> firstSnapshotCreator)
    {
        _listOfCommands = listOfCommands;
        _firstSnapshotCreator = firstSnapshotCreator;
    }

    public void CreateFirstSnapshot()
    {
        _snapshots.Add(_firstSnapshotCreator.CreateFirstSnapshot());
    }

    public void ExecuteCommands()
    {
        foreach (var commands in _listOfCommands) ExecuteAllCommands(commands);
    }

    public ProblemVisualizerSnapshots<TDrawerData> GetSnapshots()
    {
        return _snapshots;
    }

    private void ExecuteAllCommands(ProblemVisualizerCommandsQueue<TDrawerData> commandsQueue)
    {
        var drawerData = CreateCloneOfLastDrawerData();
        while (commandsQueue.Count > 0) commandsQueue.Dequeue().Execute(ref drawerData);
        _snapshots.Add(drawerData);
    }

    private TDrawerData CreateCloneOfLastDrawerData()
    {
        return (TDrawerData)_snapshots.Last().Clone();
    }
}