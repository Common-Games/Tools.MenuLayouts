using System;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public sealed class HierarchyMenuItem : MenuItem
    {
        protected override string MenuPath => "GameObject";
    }
}
