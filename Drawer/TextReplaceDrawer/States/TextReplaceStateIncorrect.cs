namespace Drawer.TextReplaceDrawer.States;

public class TextReplaceStateIncorrect : TextReplaceState
{
    public override string GetFontColor()
    {
        return "red";
    }


    public override bool IsBold()
    {
        return true;
    }

}