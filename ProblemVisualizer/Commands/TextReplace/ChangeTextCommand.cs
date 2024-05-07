using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeTextCommand(int index, string title, string value, int x, int y)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Index = index;
    public readonly string Title = title ?? "";
    public readonly string Value = value ?? "";
    public readonly int X = x;
    public readonly int Y = y;

    public override void Execute(ref GraphData data)
    {
        GraphText graphText = new($"{Title}: {Value}", X, Y);
        data.Texts[Index] = graphText;
    }
}