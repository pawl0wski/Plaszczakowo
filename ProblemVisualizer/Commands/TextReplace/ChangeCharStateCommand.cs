using Drawer.TextReplaceDrawer;
using Drawer.TextReplaceDrawer.States;

namespace ProblemVisualizer.Commands;

public class ChangeCharStateCommand(int id, TextReplaceState newState) 
    : ProblemVisualizerCommand<TextReplaceData>
{ 
    public readonly int Id = id;

    public readonly TextReplaceState State = newState;

    public override void Execute(ref TextReplaceData data)
    {
        data.Chars[Id].State = State;
    }
}