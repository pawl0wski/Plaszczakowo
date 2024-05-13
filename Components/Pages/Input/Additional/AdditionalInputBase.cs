using Microsoft.AspNetCore.Components;
using ProblemResolver;
using ProjektZaliczeniowy_AiSD2.Components.States;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.Additional;

public abstract class AdditionalInputBase<TProblemInput> : ComponentBase
    where TProblemInput : ProblemInputData
{
    [Inject] 
    protected IProblemState? ProblemState { get; set; }

    protected TProblemInput? ProblemInput;
    
    protected override async Task OnInitializedAsync()
    {
        await InitializeProblemInput();
        InitializeDefaultInputProperties();
        await base.OnInitializedAsync();
    }

    private async Task InitializeProblemInput()
    {
        if (ProblemState is null)
            throw new NullReferenceException("ProblemState is null!");
        
        ProblemInput = await ProblemState.GetProblemInputData<TProblemInput>();
    }

    protected abstract void InitializeDefaultInputProperties();
}