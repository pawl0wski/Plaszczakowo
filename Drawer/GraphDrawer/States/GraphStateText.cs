namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateText : GraphState
{
    public override string GetPrimaryColor()
    {
        return "black";
    }

    public override string GetSecondaryColor()
    {
        return "white";
    }

    public override string GetThroughputColor()
    {
        return "black";
    }

    public override int GetLineWidth()
    {
        return 1;
    }

    public override int GetOutlineWidth()
    {
        return 1;
    }

    public override int GetEdgeRadius()
    {
        return 18;
    }
}