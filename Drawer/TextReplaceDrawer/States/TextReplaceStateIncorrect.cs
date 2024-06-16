namespace Plaszczakowo.Drawer.TextReplaceDrawer.States;

public class TextReplaceStateIncorrect : TextReplaceState
{
    public override string GetFontColor()
    {
        return "red";
    }

    public override string GetBackgroundColor()
    {
        return "#ffb3b3";
    }


    public override bool IsBold()
    {
        return true;
    }
}