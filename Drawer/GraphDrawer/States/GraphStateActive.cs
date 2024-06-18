namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateActive : GraphState
{
    public override string GetPrimaryColor()
    {
        return "#059212";
    }

    public override string GetSecondaryColor()
    {
        return "#ecffed";
    }

    public override string GetThroughputColor()
    {
        return "black";
    }

    public override int GetLineWidth()
    {
        return 5;
    }
}