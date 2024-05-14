using ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput;

public static class GraphCreatorProblemSettings
{
    public static (bool, GraphInputValidatorModes) GetSettingsForProblem(string problemName)
    {
        return problemName switch
        {
            "guard_schedule" => GetSettingsForGuardSchedule(),
            "carrier_assignment" => GetSettingsForFenceTransport(),
            _ => throw new Exception($"Unknown {problemName} problemName.")
        };
    }

    private static (bool, GraphInputValidatorModes) GetSettingsForGuardSchedule() => (true,
        GraphInputValidatorModes.HaveLoop 
        | GraphInputValidatorModes.OneEdgeFromEveryVertex 
        | GraphInputValidatorModes.EverythingConnected
        | GraphInputValidatorModes.ShouldHave3Vertices);

    private static (bool, GraphInputValidatorModes) GetSettingsForFenceTransport() =>
        (false, 
            GraphInputValidatorModes.EverythingConnected 
            | GraphInputValidatorModes.ShouldHave3Vertices);
}

