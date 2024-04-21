namespace Problem.CarrierAssignment;

public class Edge 
{
    public int From;
    public int To;
    public int Flow;
    public int Capacity;

    public Edge(int from, int to, int flow = 0, int capacity = 1) {
        this.From = from;
        this.To = to;
        this.Flow = flow;
        this.Capacity = capacity;
    }
    

}