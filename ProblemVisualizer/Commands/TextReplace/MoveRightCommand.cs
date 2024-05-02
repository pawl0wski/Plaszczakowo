using Drawer.TextReplaceDrawer;

namespace ProblemVisualizer.Commands;

public class MoveRightCommand :
    ProblemVisualizerCommand<TextReplaceData>
{
    public override void Execute(ref TextReplaceData data)
    {
        data.Offset += 1;
    }
}