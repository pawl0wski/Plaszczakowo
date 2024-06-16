using Microsoft.AspNetCore.Components;
using Plaszczakowo.Drawer;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemResolver.ProblemInput;
using Plaszczakowo.ProblemVisualizer;
using Plaszczakowo.States;

namespace Plaszczakowo.Components.Pages.Problems;

public abstract class ProblemComponentBase<TInputData, TOutputData, TDrawData> : ComponentBase
    where TInputData : ProblemInputData
    where TOutputData : ProblemOutput
    where TDrawData : DrawerData
{
    protected ProblemVisualizerExecutor<TInputData, TDrawData>? Executor;

    protected FirstSnapshotCreator<TInputData, TDrawData>? FirstSnapshotCreator;

    public TInputData? InputData;

    protected TOutputData? OutputData;

    protected ProblemRecreationCommands<TDrawData> RecreationCommands = new();

    protected ProblemResolver<TInputData, TOutputData, TDrawData>? Resolver;

    [Inject] private IProblemState? ProblemState { get; set; }

    protected override void OnInitialized()
    {
        ResolveInputDataFromSessionStorage();
        UpdateInputData();
        InitializeResolver();
        InitializeFirstSnapshotCreator(InputData!);
        ResolveAndCreateSnapshots();
        InsertOutputDataIntoProblemState();
    }

    private void ResolveInputDataFromSessionStorage()
    {
        if (ProblemState is null)
            throw new NullReferenceException("ProblemState can't be null.");
        InputData = ProblemState.GetProblemInputData<TInputData>();
    }

    protected virtual void UpdateInputData()
    {
    }

    private void ResolveAndCreateSnapshots()
    {
        ResolveProblem();
        CreateDrawerDataSnapshots();
    }

    private void ResolveProblem()
    {
        if (Resolver is null)
            throw new NullReferenceException("You need to initialize Resolver before resolving problem.");

        OutputData = Resolver.Resolve(InputData!, ref RecreationCommands);
    }

    private void CreateDrawerDataSnapshots()
    {
        if (FirstSnapshotCreator is null)
            throw new NullReferenceException(
                "You need to initialize FirstSnapshotCreator before creating drawer snapshots.");
        Executor = new ProblemVisualizerExecutor<TInputData, TDrawData>(RecreationCommands.Commands,
            FirstSnapshotCreator);
        Executor.CreateFirstSnapshot();
        Executor.ExecuteCommands();
    }

    private void InsertOutputDataIntoProblemState()
    {
        if (ProblemState is null || OutputData is null)
            return;

        ProblemState.SetProblemOutputData(OutputData);
    }

    protected abstract void InitializeResolver();

    protected abstract void InitializeFirstSnapshotCreator(TInputData inputData);

    protected ProblemVisualizerSnapshots<TDrawData> GetSnapshots()
    {
        return Executor!.GetSnapshots();
    }
}