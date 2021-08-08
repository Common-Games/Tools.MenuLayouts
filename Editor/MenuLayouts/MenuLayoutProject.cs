namespace CGTK.Tools.CustomizableMenus
{
    //No CreateAssetMenu, is created by Settings UI
    internal sealed class MenuLayoutProject : MenuLayout<MenuItemProject>
    {
        private void OnValidate()
        {
            LayoutDrawer.UpdateProjectMenuLayout();
        }
    }
}
