using Plaszczakowo.ProblemResolver;

namespace Plaszczakowo.Problems.PhraseCorrection.Output;

public record PhraseCorrectionOutput : ProblemOutput
{

    
    public string FixedPhrase { get; set;} = "";
}

