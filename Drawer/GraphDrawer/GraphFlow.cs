namespace Drawer.GraphDrawer;

public class GraphThroughput
{
    public int Flow;
    public int Capacity;

    public GraphThroughput(int flow, int capacity)
    {
        Flow = flow;
        Capacity = capacity;
    }

    public override string ToString()
    {
        return $"{Flow}|{Capacity}";
    }
}