namespace Problem.PhraseCorrection;

public class PhraseCorrectionResolver :
    ProblemResolver<PhraseCorrectionInputData, PhraseCorrectionResults>
{
    public override List<PhraseCorrectionResults> Resolve(PhraseCorrectionInputData data)
    {
        PhraseCorrection correction = new(data.InputPhrase);
        PhraseCorrectionResults step = new();
        List<PhraseCorrectionResults> output = [step];
        correction.FixPhrase(ref data.InputPhrase, ref output);
        output[0].InitialPhrase = data.InputPhrase;
        return output;
    }
}