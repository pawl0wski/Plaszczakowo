namespace Plaszczakowo.Drawer.GraphDrawer.States;

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

    public override string GetThroughputColor()
    {
        return "#b7b700";
    }

    public override int GetLineWidth()
    {
        return 1;
    }
}