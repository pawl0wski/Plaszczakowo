namespace Drawer.TextReplaceDrawer.States;

public class TextReplaceStateInactive : TextReplaceState
{
    public override string GetFontColor()
    {
        return "gray";
    }

    public override bool IsBold()
    {
        return false;
    }

}