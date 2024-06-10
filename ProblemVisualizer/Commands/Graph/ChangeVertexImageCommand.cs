using Drawer.GraphDrawer;

namespace ProblemVisualizer.Commands;

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