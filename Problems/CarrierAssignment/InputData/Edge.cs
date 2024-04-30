namespace ProblemResolver.CarrierAssignment;

public class Edge
{
    public int Capacity;
    public int Flow;
    public int From;
    public int To;

    public Edge(int from, int to, int flow = 0, int capacity = 1)
    {
        From = from;
        To = to;
        Flow = flow;
        Capacity = capacity;
    }
}