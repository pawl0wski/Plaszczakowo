using Microsoft.AspNetCore.Components;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input;

public class InputChoiceBase : ComponentBase
{
    [Parameter] public required string ProblemName { get; set; }

    protected string GetFromFileLink()
    {
        return $"/input/file/{ProblemName}";
    }

    protected string GetFromKeyboardLink()
    {
        return $"/input/keyboard/{ProblemName}";
    }

    protected string GetGraphCreatorLink()
    {
        return $"/input/graphcreator/{ProblemName}";
    }
}