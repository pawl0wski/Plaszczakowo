using Drawer.TextReplaceDrawer;
using ProblemVisualizer;

namespace Problem.PhraseCorrection;

public class FirstPhraseCorrectionSnapshotCreator(PhraseCorrectionInputData inputData)
    : FirstSnapshotCreator<PhraseCorrectionInputData, TextReplaceData>(inputData)
{
    public override TextReplaceData CreateFirstSnapshot()
    {
        List<TextReplaceChar> chars = [];
        foreach( char c in InputData.InputPhrase) {
            chars.Add(new TextReplaceChar(c, null));
        }

        return new TextReplaceData(chars);
    }
}