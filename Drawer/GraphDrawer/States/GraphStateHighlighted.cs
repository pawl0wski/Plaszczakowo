namespace Plaszczakowo.Drawer.GraphDrawer.States;

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

    public override string GetThroughputColor()
    {
        return "#5454ff";
    }

    public override int GetLineWidth()
    {
        return 3;
    }
}