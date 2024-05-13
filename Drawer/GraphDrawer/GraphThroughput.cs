namespace Drawer.GraphDrawer;

public class GraphThroughput
{
    public int Flow;
    public int Capacity;

    public GraphThroughput(int flow, int capacity = -1)
    {
        Flow = flow;
        Capacity = capacity;
    }

    public override string ToString()
    {
        return Capacity == -1 ? $"{Flow}" : $"{Flow}|{Capacity}";
    }
}