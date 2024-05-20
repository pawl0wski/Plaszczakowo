using Problem.GuardSchedule;
using Problem.CarrierAssignment;
using Problem.PhraseCorrection;
using ProblemResolver.Graph;
using System.Text.Json;

public static class DefaultProblemInputProvider
{
    private const string DemoFileName = "Przykladowy problem.json";
    public static void CreateDefaultFilesForProblems()
    {
        Directory.CreateDirectory(GetProblemPath(""));
        Directory.CreateDirectory(GetProblemPath("guard_schedule"));
        Directory.CreateDirectory(GetProblemPath("computer_science_machine"));
        Directory.CreateDirectory(GetProblemPath("carrier_assignment"));
        
        if (!CheckIfFileExists("guard_schedule"))
            GenerateGuardScheduleFile();
        
        if (!CheckIfFileExists("computer_science_machine"))
            GenerateComputerScienceMachineFile();
        
        if (!CheckIfFileExists("carrier_assignment"))
            GenerateFenceTransportFile();
    }

    private static string GetProblemPath(string problemName)
    {
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Plaszczakowo",
            problemName);
    }

    private static bool CheckIfFileExists(string problemName)
    {
        return File.Exists(GetProblemPath(Path.Join(problemName, DemoFileName)));
    }

    private static void GenerateGuardScheduleFile()
    {
        string destinationFilePath = Path.Join(GetProblemPath("guard_schedule"), DemoFileName);

        List<ProblemVertex> problemVertices = 
        [
            new ProblemVertex(0, 250, 320, 5, true),
            new ProblemVertex(1, 450, 220, 10, false),
            new ProblemVertex(2, 690, 230, 12, false),
            new ProblemVertex(3, 840, 360, 10, false),
            new ProblemVertex(4, 800, 440, 8, false),
            new ProblemVertex(5, 650, 525, 17, false),
            new ProblemVertex(6, 500, 520, 3, false),
            new ProblemVertex(7, 310, 470, 13, false),
        ];

        List<ProblemEdge> problemEdges = 
        [
            new ProblemEdge(0, 0, 1, true),
            new ProblemEdge(1, 1, 2, true),
            new ProblemEdge(2, 2, 3, true),
            new ProblemEdge(3, 3, 4, true),
            new ProblemEdge(4, 4, 5, true),
            new ProblemEdge(5, 5, 6, true),
            new ProblemEdge(6, 6, 7, true),
            new ProblemEdge(7, 7, 0, true),
        ];

        List<Plaszczak> plaszczaki =
        [
            new Plaszczak(25, 0, 0),
            new Plaszczak(13, 0, 0),
            new Plaszczak(15, 0, 0),
            new Plaszczak(19, 0, 0),
            new Plaszczak(23, 0, 0),
            new Plaszczak(28, 0, 0),
            new Plaszczak(3, 0, 0),
            new Plaszczak(8, 0, 0),
            new Plaszczak(37, 0, 0),
            new Plaszczak(44, 0, 0),
        ];

        GuardScheduleInputData guardSchedule = new(problemVertices, problemEdges, plaszczaki, 3);
        string jsonGuardSchedule = JsonSerializer.Serialize(guardSchedule);
        File.WriteAllText(destinationFilePath, jsonGuardSchedule);
    }

    private static void GenerateComputerScienceMachineFile()
    {
        string destinationFilePath = Path.Join(GetProblemPath("computer_science_machine"), DemoFileName);
        string inputPhrase = "peter piper picked a picked pepper";

        PhraseCorrectionInputData computerScienceMachine = new(inputPhrase);
        string jsonComputerScienceMachine = JsonSerializer.Serialize(computerScienceMachine);
        File.WriteAllText(destinationFilePath, jsonComputerScienceMachine);
    }
    private static void GenerateFenceTransportFile()
    {
        string destinationFilePath = Path.Join(GetProblemPath("carrier_assignment"), DemoFileName);

        List<Edge> relations =
        [
            new Edge(0, 5),
            new Edge(0, 6),
            new Edge(0, 7),
            new Edge(1, 5),
            new Edge(1, 9),
            new Edge(2, 5),
            new Edge(2, 7),
            new Edge(3, 7),
            new Edge(3, 8),
            new Edge(4, 6),
        ];

        List<ProblemEdge> paths =
        [
            new ProblemEdge(0, 0, 1),
            new ProblemEdge(1, 1, 2),
            new ProblemEdge(2, 2, 3),
            new ProblemEdge(3, 2, 4),
            new ProblemEdge(4, 3, 4),
            new ProblemEdge(5, 3, 5),
            new ProblemEdge(6, 4, 6),
        ];

        List<ProblemVertex> landmarks =
        [
            new ProblemVertex(0, 50, 100, null),
            new ProblemVertex(1, 150, 250, null),
            new ProblemVertex(2, 500, 300, null),
            new ProblemVertex(3, 600, 400, null),
            new ProblemVertex(4, 600, 700, null),
            new ProblemVertex(5, 50, 700, null),
            new ProblemVertex(6, 800, 250, null),
        ];

        FenceTransportInputData carrierAssigment = new(5, 5, relations, paths, landmarks, 3);
        string jsonCarrierAssigment = JsonSerializer.Serialize(carrierAssigment);
        File.WriteAllText(destinationFilePath, jsonCarrierAssigment);
    }
}
