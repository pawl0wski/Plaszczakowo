using Microsoft.AspNetCore.Components;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods;

public class InputMethodBase : ComponentBase
{
    [Parameter] 
    public required string ProblemName { get; set; }
}