namespace GraphDrawer;
public class GraphEdge {
    public readonly GraphVertex From;

    public readonly GraphVertex To;

    public GraphState State;

    public GraphFlow? Flow;

    public GraphEdge(GraphVertex From, GraphVertex To, GraphState? state = null, GraphFlow? flow = null)
    {
        this.From = From;
        this.To = To;
        State = state ?? GraphStates.Inactive;
        Flow = flow;
    }

}