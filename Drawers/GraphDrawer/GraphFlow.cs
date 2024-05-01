namespace Drawer.GraphDrawer;

public class GraphFlow
{
    public int First;
    public int Second;

    public GraphFlow(int first, int second)
    {
        First = first;
        Second = second;
    }

    public override string ToString()
    {
        return $"{First}|{Second}";
    }
}