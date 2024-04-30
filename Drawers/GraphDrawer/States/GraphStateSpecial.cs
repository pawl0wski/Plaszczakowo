namespace Drawer.GraphDrawer;

public class GraphStateSpecial : GraphState
{
    public override string GetPrimaryColor()
    {
        return "blue";
    }

    public override string GetSecondaryColor()
    {
        return "yellow";
    }

    public override int GetLineWidth()
    {
        return 1;
    }
}