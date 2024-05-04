using ProblemResolver;

namespace Problem.PhraseCorrection;

public record PhraseCorrectionOutput : ProblemOutput
{

    
    public string FixedPhrase;


    public PhraseCorrectionOutput()
    {
        FixedPhrase = "";
    }
}

