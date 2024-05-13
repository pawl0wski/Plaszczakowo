namespace Drawer.GraphDrawer;

public abstract class GraphState
{
    public abstract string GetPrimaryColor();

    public abstract string GetSecondaryColor();
    public abstract string GetThroughputColor();

    public abstract int GetLineWidth();

    public virtual int GetOutlineWidth() => 5;

    public virtual int GetEdgeRadius() => 25;
}