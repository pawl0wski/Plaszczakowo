using Drawer;
using Microsoft.AspNetCore.Components;
using ProblemResolver;
using ProblemVisualizer;
using ProjektZaliczeniowy_AiSD2.Components.States;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Problems;

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

    protected override async Task OnInitializedAsync()
    {
        await ResolveInputDataFromSessionStorage();
        UpdateInputData();
        InitializeResolver();
        InitializeFirstSnapshotCreator(InputData!);
        ResolveAndCreateSnapshots();
        await InsertOutputDataIntoProblemState();
    }

    protected async Task ResolveInputDataFromSessionStorage()
    {
        if (ProblemState is null)
            throw new NullReferenceException("ProblemState can't be null.");
        InputData = await ProblemState.GetProblemInputData<TInputData>();
    }

    protected virtual void UpdateInputData()
    {
        
    }
    
    protected void ResolveAndCreateSnapshots()
    {
        ResolveProblem();
        CreateDrawerDataSnapshots();
    }

    protected void ResolveProblem()
    {
        if (Resolver is null)
            throw new NullReferenceException("You need to initialize Resolver before resolving problem.");

        OutputData = Resolver.Resolve(InputData!, ref RecreationCommands);
    }

    protected void CreateDrawerDataSnapshots()
    {
        if (FirstSnapshotCreator is null)
            throw new NullReferenceException(
                "You need to initialize FirstSnapshotCreator before creating drawer snapshots.");
        Executor = new ProblemVisualizerExecutor<TInputData, TDrawData>(RecreationCommands.Commands,
            FirstSnapshotCreator);
        Executor.CreateFirstSnapshot();
        Executor.ExecuteCommands();
    }
    protected async Task InsertOutputDataIntoProblemState()
    {
        if (ProblemState is null)
            return;
        
        await ProblemState.SetProblemOutputData(OutputData);
    }

    protected abstract void InitializeResolver();

    protected abstract void InitializeFirstSnapshotCreator(TInputData inputData);

    protected ProblemVisualizerSnapshots<TDrawData> GetSnapshots()
    {
        return Executor!.GetSnapshots();
    }
}