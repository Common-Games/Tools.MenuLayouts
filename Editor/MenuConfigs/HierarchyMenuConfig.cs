using System;
using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
    //No CreateAssetMenu, is created by Settings UI
    [CreateAssetMenu]
    public sealed class HierarchyMenuConfig : MenuConfig<HierarchyMenuItem>
    {
        protected override String ConfigName => nameof(HierarchyMenuConfig);
    }
}
