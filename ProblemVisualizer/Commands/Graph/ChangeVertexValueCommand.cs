using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

public class ChangeVertexValueCommand(int id, string value)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly string Value = value;

    public override void Execute(ref GraphData data)
    {
        data.ChangeVertexText(Id, Value);
    }
}