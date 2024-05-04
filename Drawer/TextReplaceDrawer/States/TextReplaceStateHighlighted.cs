namespace Drawer.TextReplaceDrawer.States;

public class TextReplaceStateHighlighted : TextReplaceState
{
    public override string GetFontColor()
    {
        return "orange";
    }

    public override string GetBackgroundColor()
    {
        return "#ffe7c9";
    }

    public override bool IsBold()
    {
        return true;
        
    }

}