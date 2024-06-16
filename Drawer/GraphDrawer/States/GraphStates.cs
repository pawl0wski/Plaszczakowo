namespace Plaszczakowo.Drawer.GraphDrawer.States;

public static class GraphStates
{
    public static readonly GraphStateActive Active = new();

    public static readonly GraphStateHighlighted Highlighted = new();

    public static readonly GraphStateInactive Inactive = new();

    public static readonly GraphStateSpecial Special = new();

    public static readonly GraphStateText Text = new();
}