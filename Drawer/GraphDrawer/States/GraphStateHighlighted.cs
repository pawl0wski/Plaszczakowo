namespace Drawer.GraphDrawer;

public class GraphStateHighlighted : GraphState
{
    public override string GetPrimaryColor() => "blue";

    public override string GetSecondaryColor() => "white";

    public override string GetThroughputColor() => "#5454ff";

    public override int GetLineWidth() => 3;
}