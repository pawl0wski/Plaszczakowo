using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class AddNewTextCommand(int x, int y, string newText, GraphState? state)
    : ProblemVisualizerCommand<GraphData>
{

    public override void Execute(ref GraphData data)
    {
        GraphText text = new(newText, x, y, state);
        data.Texts.Add(text);

    }
}