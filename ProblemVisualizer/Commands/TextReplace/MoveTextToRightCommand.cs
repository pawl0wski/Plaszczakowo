using Drawer.TextReplaceDrawer;

namespace ProblemVisualizer.Commands;

public class MoveTextToRightCommand :
    ProblemVisualizerCommand<TextReplaceData>
{
    public override void Execute(ref TextReplaceData data)
    {
        data.Offset += 1;
    }
}