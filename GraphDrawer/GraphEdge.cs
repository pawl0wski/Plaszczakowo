public class GraphEdge {
    public readonly  GraphVertex From;

    public readonly  GraphVertex To;

    public GraphState State;

    public GraphEdge(GraphVertex From, GraphVertex To, GraphState? state = null) {
        this.From = From;
        this.To = To;
        State = state ?? GraphStates.Inactive;
    }

}