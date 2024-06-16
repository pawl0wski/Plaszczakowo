using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;

namespace Plaszczakowo.ProblemResolver.ProblemGraph;

public class ProblemVertex(int id, int? x, int? y, int? value, bool isSpecial = false) : ICloneable
{
    public int Id { get; set; } = id;

    public int? X { get; set; } = x;

    public int? Y { get; set; } = y;

    public int? Value { get; set; } = value;

    public bool IsSpecial { get; set; } = isSpecial;

    public object Clone()
    {
        return new ProblemVertex(Id, X, Y, Value);
    }

    public static ProblemVertex FromGraphVertex(int id, GraphVertex vertex)
    {
        return new ProblemVertex(id,
            vertex.X,
            vertex.Y,
            vertex.Value is null ? null : Convert.ToInt32(vertex.Value),
            vertex.State == GraphStates.Special);
    }

    public GraphVertex ToGraphVertex()
    {
        return new GraphVertex(X ?? 0, Y ?? 0, Value?.ToString(),
            IsSpecial ? GraphStates.Special : GraphStates.Inactive);
    }
}