using Drawer.GraphDrawer;

namespace ProblemResolver.Graph;

public record ProblemVertex(int Id, int? X, int? Y)
{
    public int Id { get; set; } = Id;

    public int? X { get; set; } = X;

    public int? Y { get; set; } = Y;

    public static ProblemVertex FromGraphVertex(int id, GraphVertex vertex)
    {
        return new ProblemVertex(id, vertex.X, vertex.Y);
    }
}