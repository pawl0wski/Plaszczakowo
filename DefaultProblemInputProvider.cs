using Problem.GuardSchedule;
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
        Directory.CreateDirectory(GetProblemPath("fence_transport"));
        
        if (!CheckIfFileExists("guard_schedule"))
            GenerateGuardScheduleFile();
        
        if (!CheckIfFileExists("computer_science_machine"))
            GenerateComputerScienceMachineFile();
        
        if (!CheckIfFileExists("fence_transport"))
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
            new ProblemVertex(0, 390, 200, 4),
            new ProblemVertex(1, 610, 170, 7),
            new ProblemVertex(2, 690, 300, 13),
            new ProblemVertex(3, 590, 400, 10),
            new ProblemVertex(4, 440, 440, 8),
            new ProblemVertex(5, 310, 349, 17),
        ];

        List<ProblemEdge> problemEdges = 
        [
            new ProblemEdge(0, 0, 1),
            new ProblemEdge(1, 1, 2),
            new ProblemEdge(2, 2, 3),
            new ProblemEdge(3, 3, 4),
            new ProblemEdge(4, 4, 5),
            new ProblemEdge(5, 5, 0),
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
    }
    private static void GenerateFenceTransportFile()
    {
        string destinationFilePath = Path.Join(GetProblemPath("fence_transport"), DemoFileName);

    }
}
