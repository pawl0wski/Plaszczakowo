namespace Plaszczakowo.Drawer.GraphDrawer.States;

public class GraphStateActive : GraphState
{
    public override string GetPrimaryColor() => "green";

    public override string GetSecondaryColor() => "white";

    public override string GetThroughputColor() => "brown";

    public override int GetLineWidth() => 5;
}