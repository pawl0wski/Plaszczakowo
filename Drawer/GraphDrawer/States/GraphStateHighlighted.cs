namespace Drawer.GraphDrawer;

public class GraphStateHighlighted : GraphState
{
    public override string GetPrimaryColor()
    {
        return "blue";
    }

    public override string GetSecondaryColor()
    {
        return "white";
    }

    public override int GetLineWidth()
    {
        return 3;
    }
}