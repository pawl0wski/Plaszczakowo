namespace Plaszczakowo.Drawer.GraphDrawer.States;

public abstract class GraphState
{
    public abstract string GetPrimaryColor();

    public abstract string GetSecondaryColor();
    public abstract string GetThroughputColor();

    public abstract int GetLineWidth();

    public virtual int GetOutlineWidth()
    {
        return 5;
    }

    public virtual int GetEdgeRadius()
    {
        return 25;
    }
}