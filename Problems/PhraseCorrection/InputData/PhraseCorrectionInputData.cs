using ProblemResolver;

namespace Problem.PhraseCorrection;

public record PhraseCorrectionInputData : ProblemInputData
{
    public string InputPhrase { get; set; }

    public PhraseCorrectionInputData(string inputPhrase)
    {
        this.InputPhrase = inputPhrase;
    }
}

