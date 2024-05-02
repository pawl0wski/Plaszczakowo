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
                string destinationDirectory = GetProblemPath(p);

                
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
