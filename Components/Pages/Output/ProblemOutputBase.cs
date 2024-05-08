using Microsoft.AspNetCore.Components;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Output;

public class ProblemOutputBase : ComponentBase
{
    [Parameter] public required string ProblemName { get; set; }
}
