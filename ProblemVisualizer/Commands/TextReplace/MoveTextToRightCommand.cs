using Plaszczakowo.Drawer.TextReplaceDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class MoveTextToRightCommand :
    ProblemVisualizerCommand<TextReplaceData>
{
    public override void Execute(ref TextReplaceData data)
    {
        data.Offset += 1;
    }
}