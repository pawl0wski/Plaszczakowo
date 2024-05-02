using Drawer.TextReplaceDrawer.States;

namespace Drawer.TextReplaceDrawer;

public record TextReplaceChar (char Content, TextReplaceState? State = null) {
    public char Content = Content;

    public TextReplaceState State = State ?? new TextReplaceStateInactive();
}