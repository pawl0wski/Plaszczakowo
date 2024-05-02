namespace Drawer.TextReplaceDrawer.States;

public class TextReplaceStateHighlighted : TextReplaceState
{
    public override string GetFontColor()
    {
        return "black";
    }

    public override bool IsBold()
    {
        return true;
        
    }

}