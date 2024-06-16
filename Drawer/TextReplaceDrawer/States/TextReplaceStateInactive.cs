namespace Plaszczakowo.Drawer.TextReplaceDrawer.States;

public class TextReplaceStateInactive : TextReplaceState
{
    public override string GetFontColor()
    {
        return "black";
    }

    public override string GetBackgroundColor()
    {
        return "white";
    }

    public override bool IsBold()
    {
        return false;
    }

}