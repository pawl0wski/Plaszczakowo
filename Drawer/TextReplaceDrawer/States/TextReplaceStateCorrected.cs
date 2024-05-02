namespace Drawer.TextReplaceDrawer.States;
public class TextReplaceStateCorrected : TextReplaceState
{
    public override string GetFontColor()
    {
        return "green";
    }

    public override bool IsBold()
    {
        return true;
    }

}