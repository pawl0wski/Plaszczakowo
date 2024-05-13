namespace Drawer.GraphDrawer;

public class GraphStateSpecial : GraphState
{
    public override string GetPrimaryColor() => "blue";

    public override string GetSecondaryColor() => "yellow";

    public override string GetThroughputColor() => "#b7b700";

    public override int GetLineWidth() => 1;
}