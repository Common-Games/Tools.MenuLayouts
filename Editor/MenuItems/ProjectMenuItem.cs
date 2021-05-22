using System;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public sealed class ProjectMenuItem : MenuItem
    {
        protected override string MenuPath => "Assets";
    }
}
