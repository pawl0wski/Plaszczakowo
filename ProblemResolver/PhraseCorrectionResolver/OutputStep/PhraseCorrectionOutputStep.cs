namespace Problem.PhraseCorrection;

public class PhraseCorrectionOutputStep : ProblemOutputStep
{
    public string InitialPhrase;
    public List<Tuple<char, char>> FixingPhrase;
    public string FixedPhrase;


    public PhraseCorrectionOutputStep()
    {
        this.FixingPhrase = new();
        this.FixedPhrase = "";
        this.InitialPhrase = "";
    }
}