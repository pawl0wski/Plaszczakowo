using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeTextCommand(int index, string text, int x, int y, GraphState? state = null)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Index = index;
    public readonly GraphState? State = state;
    public readonly string Text = text;
    public readonly int X = x;
    public readonly int Y = y;

    public override void Execute(ref GraphData data)
    {
        GraphText graphText = new(Text, X, Y);
        data.Texts[Index] = graphText;
        data.Texts[Index].State = State ?? GraphStates.Inactive;
    }
}