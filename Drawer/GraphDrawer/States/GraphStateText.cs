namespace Drawer.GraphDrawer;

public class GraphStateText : GraphState
{
    public override string GetPrimaryColor() => "black";

    public override string GetSecondaryColor() => "white";

    public override string GetThroughputColor() => "gray";

    public override int GetLineWidth() => 1;
    
    public override int GetOutlineWidth() => 1;

    public override int GetEdgeRadius() => 20;
}