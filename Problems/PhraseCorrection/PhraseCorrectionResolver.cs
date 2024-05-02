using Drawer.TextReplaceDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;
namespace Problem.PhraseCorrection;

public class PhraseCorrectionResolver : ProblemResolver<PhraseCorrectionInputData, PhraseCorrectionOutput, TextReplaceData>
{
    public override PhraseCorrectionOutput Resolve(PhraseCorrectionInputData data, ref ProblemRecreationCommands<TextReplaceData> commands)
    {
        PhraseCorrection correction = new(data.InputPhrase);
        PhraseCorrectionOutput output = new();
        correction.FixPhrase(data.InputPhrase, ref output, ref commands);
        return output;
    }
}

