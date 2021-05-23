using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
    //No CreateAssetMenu, is created by Settings UI
    [CreateAssetMenu]
    public sealed class HierarchyMenuConfig : MenuConfig<HierarchyMenuItem>
    {
        protected override string ConfigName => nameof(HierarchyMenuConfig);
    }
}
