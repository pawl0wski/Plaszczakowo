using Microsoft.AspNetCore.Components;
using ProblemResolver;
using ProjektZaliczeniowy_AiSD2.States;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.Additional;

public abstract class AdditionalInputBase<TProblemInput> : ComponentBase
    where TProblemInput : ProblemInputData
{
    [Inject] 
    protected IProblemState? ProblemState { get; set; }

    protected TProblemInput? ProblemInput;
    
    protected override async Task OnInitializedAsync()
    {
        InitializeProblemInput();
        InitializeDefaultInputProperties();
        await base.OnInitializedAsync();
    }

    private void InitializeProblemInput()
    {
        if (ProblemState is null)
            throw new NullReferenceException("ProblemState is null!");
        
        ProblemInput = ProblemState.GetProblemInputData<TProblemInput>();
    }

    protected abstract void InitializeDefaultInputProperties();
}