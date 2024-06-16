using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.Problems.PhraseCorrection.Input;

public record PhraseCorrectionInputData(string InputPhrase) : ProblemInputData
{
    public string InputPhrase { get; set; } = InputPhrase;
}

