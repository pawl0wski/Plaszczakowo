using ProblemResolver;

namespace Problem.PhraseCorrection;

public record PhraseCorrectionInputData(string InputPhrase) : ProblemInputData
{
    public string InputPhrase { get; set; } = InputPhrase;
}

