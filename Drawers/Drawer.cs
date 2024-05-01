namespace Drawer;

public abstract class Drawer
{
    public abstract Task Draw();

    public abstract void ChangeDrawerData(DrawerData drawerData);
}