using System;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public sealed class MenuItemHierarchy : MenuItem
    {
        protected override String MenuPath => "GameObject";
    }
}
