namespace Plaszczakowo.Drawer.GraphDrawer.States;

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

    public override string GetThroughputColor()
    {
        return "gray";
    }

    public override int GetLineWidth()
    {
        return 1;
    }
}