namespace Drawer.TextReplaceDrawer;

public class TextReplaceData : DrawerData
{

    public int Offset = 0;
    public List<TextReplaceChar> Chars;

    public TextReplaceData(List<TextReplaceChar> chars)
    {
        Chars = chars;
    }


    public override TextReplaceData Clone()
    {
        return new TextReplaceData(Chars);
    }
}
