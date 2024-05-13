using ProblemResolver;

namespace Problem.PhraseCorrection;

public record PhraseCorrectionOutput : ProblemOutput
{

    
    public string FixedPhrase { get; set;}


    public PhraseCorrectionOutput()
    {
        FixedPhrase = "";
    }
}

