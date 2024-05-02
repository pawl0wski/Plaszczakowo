using Problem.GuardSchedule;
using ProblemResolver.Graph;
using System.Text.Json;

namespace ProblemInput;

public class DefaultProblemInputProvider
{
    public void CreateDefaultFilesForProblems(string[] problems)
    {
        foreach (var p in problems)
        {
            if (!CheckIfDirectoryExists(p))
            {
                Directory.CreateDirectory(GetProblemPath(p));
            }
            if (!CheckIfFileExists(p))
            {
                string destinationFilePath = Path.Join(GetProblemPath(p), $"0DEMO{p}.json");

                switch (p)
                {
                    case "guard_schedule":
                        GenerateGuardScheduleFile(destinationFilePath);
                        break;
                    case "computer_science_machine":
                        break;
                    case "fence_transport":
                        break;
                }
            }
        }
    }
    private bool CheckIfDirectoryExists(string ProblemName)
    {
        return Path.Exists(GetProblemPath(ProblemName));
    }

    private string GetProblemPath(string ProblemName)
    {
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Plaszczakowo",
            ProblemName);
    }

    private bool CheckIfFileExists(string ProblemName)
    {
        return File.Exists(GetProblemPath(Path.Join(ProblemName, $"0DEMO{ProblemName}.json")));
    }

    private void GenerateGuardScheduleFile(string destinationFilePath)
    {
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

        GuardScheduleInputData guard_schedule = new(plaszczaki, problemVertices, problemEdges, 0);
        string jsonGuardSchedule = JsonSerializer.Serialize(guard_schedule);
        File.WriteAllText(destinationFilePath, jsonGuardSchedule);
    }
}
