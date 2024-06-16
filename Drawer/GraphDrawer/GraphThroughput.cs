namespace Plaszczakowo.Drawer.GraphDrawer;

public class GraphThroughput(int flow, int capacity = -1)
{
    public int Flow = flow;
    public int Capacity = capacity;

    public override string ToString()
    {
        return Capacity == -1 ? $"{Flow}" : $"{Flow}|{Capacity}";
    }
}