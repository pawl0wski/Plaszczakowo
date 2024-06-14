namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

[Flags]
public enum GraphInputValidatorModes
{
    HaveLoop = 1,
    OneEdgeFromEveryVertex = 2,
    EverythingConnected = 4,
    ShouldHave3Vertices = 8,
    ShouldHaveSpecialVertex = 16,
    EveryVertexShouldHaveValue = 32,
}