namespace Problem.PhraseCorrection;

public class PhraseCorrectionResolver :
    ProblemResolver<PhraseCorrectionInputData, PhraseCorrectionOutputStep>
{
    public override List<PhraseCorrectionOutputStep> Resolve(PhraseCorrectionInputData data)
    {
        PhraseCorrection correction = new(data.InputPhrase);
        PhraseCorrectionOutputStep step = new();
        List<PhraseCorrectionOutputStep> output = [step];
        correction.FixPhrase(ref data.InputPhrase, ref output);
        output[0].InitialPhrase = data.InputPhrase;
        return output;
    }
}