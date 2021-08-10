using System;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public sealed class MenuItemProject : MenuItem
    {
        protected override String MenuPath => "Assets";
    }
}
