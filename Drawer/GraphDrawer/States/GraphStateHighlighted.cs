namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateHighlighted : GraphState
{
    public override string GetPrimaryColor()
    {
        return "#1f5fe3";
    }

    public override string GetSecondaryColor()
    {
        return "#e5edff";
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