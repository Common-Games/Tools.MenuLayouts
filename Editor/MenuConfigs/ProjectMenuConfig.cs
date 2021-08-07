using System;
using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
    //No CreateAssetMenu, is created by Settings UI
    [CreateAssetMenu]
    public sealed class ProjectMenuConfig : MenuConfig<ProjectMenuItem>
    {
        protected override String ConfigName => nameof(ProjectMenuConfig);
    }
}
