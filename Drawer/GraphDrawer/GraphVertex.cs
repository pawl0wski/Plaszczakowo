using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertex : ICloneable
{
    public GraphState State;
    public string? Value;
    public ElementReference? VertexImageRef;
    public int X;
    public int Y;

    public GraphVertex(int x, int y, string? value = null, GraphState? state = null,
        ElementReference? vertexImageRef = null)
    {
        X = x;
        Y = y;
        Value = value;
        State = state ?? GraphStates.Inactive;
        VertexImageRef = vertexImageRef;
    }

    public object Clone()
    {
        return new GraphVertex(X, Y, Value, State, VertexImageRef);
    }
}