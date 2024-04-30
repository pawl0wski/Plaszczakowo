namespace Drawer.GraphDrawer;
public class GraphEdge : ICloneable {
    public readonly GraphVertex From;

    public readonly GraphVertex To;

    public GraphState State;

    public GraphFlow? Flow;

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