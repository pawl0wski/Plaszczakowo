namespace Drawer.GraphDrawer;

public class GraphStateActive : GraphState
{
    public override string GetPrimaryColor()
    {
        return "green";
    }

    public override string GetSecondaryColor()
    {
        return "white";
    }

    public override int GetLineWidth()
    {
        return 5;
    }
}