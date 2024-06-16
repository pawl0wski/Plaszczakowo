using Plaszczakowo.Drawer.TextReplaceDrawer;
using Plaszczakowo.Problems.PhraseCorrection.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.PhraseCorrection;

public class FirstPhraseCorrectionSnapshotCreator(PhraseCorrectionInputData inputData)
    : FirstSnapshotCreator<PhraseCorrectionInputData, TextReplaceData>(inputData)
{
    public override TextReplaceData CreateFirstSnapshot()
    {
        List<TextReplaceChar> chars = [];
        foreach (var c in InputData.InputPhrase) chars.Add(new TextReplaceChar(c));

        return new TextReplaceData(chars);
    }
}