using Microsoft.AspNetCore.Components;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input;

public class InputChoicePageBase : ComponentBase
{
    [Parameter] 
    public required string ProblemName { get; set; }

    protected string GetFromFileLink() => $"/input/file/{ProblemName}";

    protected string GetFromKeyboardLink() => $"/input/keyboard/{ProblemName}";

    protected string GetGraphCreatorLink() => $"/input/graphcreator/{ProblemName}";
}