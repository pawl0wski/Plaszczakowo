using Plaszczakowo.Drawer.GraphDrawer;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class RemoveVertexImageCommand(int id)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;

    public override void Execute(ref GraphData data)
    {
        if (Id > data.Edges.Count || Id < 0) 
        {
            return;
        }
        data.RemoveVertexImage(Id);
    }
}