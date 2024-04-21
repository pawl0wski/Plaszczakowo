namespace GraphDrawer;
public class GraphStateInactive : GraphState
{
    public override string GetPrimaryColor() => "black";

    public override string GetSecondaryColor() => "white";

    public override int GetLineWidth() => 1;
}