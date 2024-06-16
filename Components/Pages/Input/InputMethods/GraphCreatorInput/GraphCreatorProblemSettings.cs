using Plaszczakowo.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

namespace Plaszczakowo.Components.Pages.Input.InputMethods.GraphCreatorInput;

public record GraphCreatorProblemSettings
{
    public bool DirectedGraph { get; set; }

    public bool CanChangeVertexValue { get; set; }

    public bool ReadGraphDataFromFenceState { get; set; }

    public GraphInputValidatorModes Modes { get; set; }
}