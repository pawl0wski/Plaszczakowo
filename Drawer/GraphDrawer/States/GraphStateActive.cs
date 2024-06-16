namespace Plaszczakowo.Drawer.GraphDrawer.States;

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

    public override string GetThroughputColor()
    {
        return "brown";
    }

    public override int GetLineWidth()
    {
        return 5;
    }
}