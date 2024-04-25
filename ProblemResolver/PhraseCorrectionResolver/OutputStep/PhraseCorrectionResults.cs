namespace Problem.PhraseCorrection;

public class PhraseCorrectionResults : ProblemResults
{
    public string InitialPhrase;
    public List<Tuple<char, char>> FixingPhrase;
    public string FixedPhrase;


    public PhraseCorrectionResults()
    {
        this.FixingPhrase = new();
        this.FixedPhrase = "";
        this.InitialPhrase = "";
    }
}