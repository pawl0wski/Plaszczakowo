using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeTextCommand(int index, string text, int x, int y, GraphState state)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Index = index;
    public readonly string Text = text;
    public readonly int X = x;
    public readonly int Y = y;
    public readonly GraphState State = state;

    public override void Execute(ref GraphData data)
    {
        GraphText graphText = new(Text, X, Y);
        data.Texts[Index] = graphText;
        data.Texts[Index].State = state;
    }
}