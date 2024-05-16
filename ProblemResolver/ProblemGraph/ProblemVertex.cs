using Drawer.GraphDrawer;

namespace ProblemResolver.Graph;

public class ProblemVertex(int Id, int? X, int? Y, int? Value) : ICloneable
{
    public int Id { get; set; } = Id;

    public int? X { get; set; } = X;

    public int? Y { get; set; } = Y;

    public int? Value { get; set; } = Value;

    public static ProblemVertex FromGraphVertex(int id, GraphVertex vertex)
    {
        return new ProblemVertex(id, vertex.X, vertex.Y, Convert.ToInt32(vertex.Value));
    }

    public object Clone()
    {
        return new ProblemVertex(Id, X, Y, Value);
    }
}
