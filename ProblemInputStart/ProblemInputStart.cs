using Problem.Demo;

namespace ProblemInput;

public class ProblemInputStart
{
    public ProblemInputStart(string[] problems)
    {
        foreach (var p in problems)
        {
            if (!CheckIfDirectoryExists(p))
            {
                Directory.CreateDirectory(GetProblemPath(p));
            }
            if (!CheckIfFileExists(p))
            {
                string fileToCopy = $"./ProblemInputStart/DemoInputs/0DEMO{p}.json";
                string destinationDirectory = GetProblemPath(p);

                if (File.Exists(fileToCopy))
                {
                    CopyFile(fileToCopy, destinationDirectory, p);
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

    private void CopyFile(string fileToCopy, string destinationDirectory, string p)
    {
        File.Copy(fileToCopy, Path.Combine(destinationDirectory, $"0DEMO{p}.json"), true);
    }
}
