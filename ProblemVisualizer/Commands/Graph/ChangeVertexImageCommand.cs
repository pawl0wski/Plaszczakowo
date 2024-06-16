using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.Images;

namespace Plaszczakowo.ProblemVisualizer.Commands;

public class ChangeVertexImageCommand(int id, GraphVertexImage image)
    : ProblemVisualizerCommand<GraphData>
{
    public readonly int Id = id;
    public readonly GraphVertexImage Image = image;

    public override void Execute(ref GraphData data)
    {
        if (Id > data.Edges.Count || Id < 0) 
        {
            return;
        }
        data.ChangeVertexImage(Id, Image);
    }
}