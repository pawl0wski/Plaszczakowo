namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateSpecial : GraphState
{
    public override string GetPrimaryColor()
    {
        return "#6932a8";
    }

    public override string GetSecondaryColor()
    {
        return "#e0c5ff";
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