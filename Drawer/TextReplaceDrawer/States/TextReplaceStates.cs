namespace Drawer.TextReplaceDrawer.States;

public static class TextReplaceStates
{
    public static readonly TextReplaceStateCorrected Corrected = new();

    public static readonly TextReplaceStateHighlighted Highlighted = new();

    public static readonly TextReplaceStateInactive Inactive = new();

    public static readonly TextReplaceStateIncorrect Incorrect = new();
}