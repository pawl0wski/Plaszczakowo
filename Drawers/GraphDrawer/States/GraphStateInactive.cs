namespace Drawer.GraphDrawer;

public class GraphStateInactive : GraphState
{
    public override string GetPrimaryColor()
    {
        return "black";
    }

    public override string GetSecondaryColor()
    {
        return "white";
    }

    public override int GetLineWidth()
    {
        return 1;
    }
}