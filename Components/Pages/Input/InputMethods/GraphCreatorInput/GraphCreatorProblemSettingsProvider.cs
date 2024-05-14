using ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput;

public static class GraphCreatorProblemSettingsProvider
{
    public static GraphCreatorProblemSettings GetSettingsForProblem(string problemName)
    {
        return problemName switch
        {
            "guard_schedule" => GetSettingsForGuardSchedule(),
            "carrier_assignment" => GetSettingsForFenceTransport(),
            _ => throw new Exception($"Unknown {problemName} problemName.")
        };
    }

    private static GraphCreatorProblemSettings GetSettingsForGuardSchedule() => new GraphCreatorProblemSettings
    {
        DirectedGraph = true,
        CanChangeVertexValue = true,
        Modes = GraphInputValidatorModes.HaveLoop
                | GraphInputValidatorModes.OneEdgeFromEveryVertex
                | GraphInputValidatorModes.EverythingConnected
                | GraphInputValidatorModes.ShouldHave3Vertices
    };


    private static GraphCreatorProblemSettings GetSettingsForFenceTransport() => new GraphCreatorProblemSettings
    {
        DirectedGraph = false,
        CanChangeVertexValue = false,
        Modes = GraphInputValidatorModes.EverythingConnected
                | GraphInputValidatorModes.ShouldHave3Vertices
    };
}