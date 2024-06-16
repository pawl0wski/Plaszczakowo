using Plaszczakowo.Drawer.TextReplaceDrawer.States;

namespace Plaszczakowo.Drawer.TextReplaceDrawer;

public class TextReplaceChar(char content, TextReplaceState? state = null) : ICloneable
{
    public char Content = content;

    public TextReplaceState State = state ?? new TextReplaceStateInactive();

    public object Clone()
    {
        return new TextReplaceChar(Content, State);
    }
}