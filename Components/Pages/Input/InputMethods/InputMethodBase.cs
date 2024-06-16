using Microsoft.AspNetCore.Components;

namespace Plaszczakowo.Components.Pages.Input.InputMethods;

public class InputMethodBase : ComponentBase
{
    [Parameter] public required string ProblemName { get; set; }
}