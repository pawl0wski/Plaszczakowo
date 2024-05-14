namespace Drawer.GraphDrawer;

public class GraphEdge : ICloneable
{
    public readonly GraphVertex From;

    public readonly GraphVertex To;

    public readonly bool Directed;
    
    public GraphThroughput? Throughput;

    public GraphState State;

    public GraphEdge(GraphVertex from, GraphVertex to, GraphState? state = null, GraphThroughput? throughput = null, bool directed = false)
    {
        From = from;
        To = to;
        State = state ?? GraphStates.Inactive;
        Throughput = throughput;
        Directed = directed;
    }

    public object Clone()
    {
        return new GraphEdge(From, To, State, Throughput, Directed);
    }
}