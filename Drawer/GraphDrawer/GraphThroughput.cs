namespace Plaszczakowo.Drawer.GraphDrawer;

public class GraphThroughput(int flow, int capacity = -1)
{
    public int Capacity = capacity;
    public int Flow = flow;

    public override string ToString()
    {
        return Capacity == -1 ? $"{Flow}" : $"{Flow} / {Capacity}";
    }
}