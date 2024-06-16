using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.Drawer.GraphDrawer;

public class GraphText : ICloneable
{
    public GraphState State;
    public string Text;
    public int X;
    public int Y;

    public GraphText(string text, int x, int y, GraphState? state = null)
    {
        Text = text;
        X = x;
        Y = y;
        State = state ?? GraphStates.Inactive;
    }

    public object Clone()
    {
        return new GraphText(
            Text.Clone() as string ?? "",
            X,
            Y,
            State
        );
    }
}