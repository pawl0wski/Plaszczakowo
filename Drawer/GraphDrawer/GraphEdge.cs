namespace Drawer.GraphDrawer;

public class GraphEdge : ICloneable
{
    public readonly GraphVertex From;

    public readonly GraphVertex To;

    public GraphFlow? Flow;

    public GraphState State;

    public GraphEdge(GraphVertex from, GraphVertex to, GraphState? state = null, GraphFlow? flow = null)
    {
        From = from;
        To = to;
        State = state ?? GraphStates.Inactive;
        Flow = flow;
    }

    public object Clone()
    {
        return new GraphEdge(From, To, State, Flow);
    }
}