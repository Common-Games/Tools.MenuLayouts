using System;

namespace CGTK.Tools.CustomizableMenus
{
    //No CreateAssetMenu, is created by Settings UI
    internal sealed class MenuLayoutHierarchy : MenuLayout<MenuItemHierarchy>
    {
        private void OnValidate()
        {
            LayoutDrawer.UpdateHierarchyMenuLayout();
        }
    }
}
