namespace Plaszczakowo.Drawer.TextReplaceDrawer;

public class TextReplaceData : DrawerData
{
    public List<TextReplaceChar> Chars;

    public int Offset;

    public TextReplaceData(List<TextReplaceChar> chars, int? offset = null)
    {
        Chars = chars;
        Offset = offset ?? 0;
    }


    public override TextReplaceData Clone()
    {
        List<TextReplaceChar> chars = [];
        chars.AddRange(Chars.Select(c => (TextReplaceChar)c.Clone()));
        return new TextReplaceData(chars, Offset);
    }
}