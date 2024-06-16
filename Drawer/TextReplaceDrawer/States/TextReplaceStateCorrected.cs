namespace Plaszczakowo.Drawer.TextReplaceDrawer.States;

public class TextReplaceStateCorrected : TextReplaceState
{
    public override string GetFontColor()
    {
        return "green";
    }

    public override string GetBackgroundColor()
    {
        return "#9ee69e";
    }

    public override bool IsBold()
    {
        return true;
    }
}