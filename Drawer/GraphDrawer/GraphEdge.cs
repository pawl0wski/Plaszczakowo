namespace Drawer.GraphDrawer;

public class GraphEdge : ICloneable
{
    public readonly GraphVertex From;

    public readonly GraphVertex To;

    public GraphThroughput? Throughput;

    public GraphState State;

    public GraphEdge(GraphVertex from, GraphVertex to, GraphState? state = null, GraphThroughput? throughput = null)
    {
        From = from;
        To = to;
        State = state ?? GraphStates.Inactive;
        Throughput = throughput;
    }

    public object Clone()
    {
        return new GraphEdge(From, To, State, Throughput);
    }
}