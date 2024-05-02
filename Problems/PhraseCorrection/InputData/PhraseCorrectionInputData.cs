using ProblemResolver;

namespace Problem.PhraseCorrection;

public record PhraseCorrectionInputData : ProblemInputData
{
    public string InputPhrase;
    public PhraseCorrectionInputData(string inputPhrase)
    {
        this.InputPhrase = inputPhrase;
    }
}

