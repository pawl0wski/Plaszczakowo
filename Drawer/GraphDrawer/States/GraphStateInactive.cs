namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateInactive : GraphState
{
    public override string GetPrimaryColor() => "black";

    public override string GetSecondaryColor() => "white";

    public override string GetThroughputColor() => "gray";

    public override int GetLineWidth() => 1;
}