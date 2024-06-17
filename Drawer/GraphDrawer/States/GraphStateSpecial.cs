namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateSpecial : GraphState
{
    public override string GetPrimaryColor()
    {
        return "red";
    }

    public override string GetSecondaryColor()
    {
        return "white";
    }

    public override string GetThroughputColor()
    {
        return "#b7b700";
    }

    public override int GetLineWidth()
    {
        return 1;
    }
}