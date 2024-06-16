using Plaszczakowo.Drawer.TextReplaceDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeCharCommand(int id, char newChar) :
 ProblemVisualizerCommand<TextReplaceData>
{
    public readonly int Id = id;
    
    public readonly char Char = newChar;

    public override void Execute(ref TextReplaceData data)
    {
        data.Chars[Id].Content = Char;
    }
}
