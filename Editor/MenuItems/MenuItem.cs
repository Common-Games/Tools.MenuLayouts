using System;
using System.Linq;

using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using CGTK.Utils.Extensions;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public abstract partial class MenuItem
    {
        #region Fields
			
        public enum ElementType { Separator, Path }
			
        #if ODIN_INSPECTOR
        [field: HorizontalGroup, HideLabel]
        [field: LabelWidth(15)]
        #endif
        [field: SerializeField]
        public ElementType Type { get; private set; } = ElementType.Separator;

        /// <summary> Original Path. </summary>
        #if ODIN_INSPECTOR
        [field: ShowIf(condition: nameof(Type), ElementType.Path)]
        [field: HorizontalGroup, HideLabel]
        [field: ValueDropdown(valuesGetter: nameof(MenuOptions))]
        [field: OnValueChanged(action: nameof(SetDefaultCustomOnOriginalChange))]
        #endif
        [field: SerializeField]
        public String Original { get; private set; }
        
        /// <summary> Customized Path. </summary>
        #if ODIN_INSPECTOR
        [field: ShowIf(condition: nameof(Type), ElementType.Path)]
        [field: HorizontalGroup, HideLabel]
        #endif
        [field: SerializeField]
        public String Custom { get; private set; }

        #endregion

        #region Methods
			
        protected abstract String MenuPath { get; }
        
        /// <summary> Gets a tree-view of all the submenus of "<see cref="MenuPath"/>". </summary>
        /// <returns> A tree-view of all the submenus of "<see cref="MenuPath"/>". </returns>
        private String[] MenuOptions => Unsupported.GetSubmenus(menuPath: MenuPath);

        private void SetDefaultCustomOnOriginalChange()
        {
            if (Custom.NotNullOrEmpty()) return;
            Custom = Original.Split(separator: '/').Last();
        }

        #endregion
    }
}
