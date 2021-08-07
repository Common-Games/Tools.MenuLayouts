using System;
using CGTK.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public abstract partial class MenuItem
    {
        public enum ElementType
        {
            Separator,
            Path,
        }
        
        #region Fields

        #if ODIN_INSPECTOR
        [TableColumnWidth(width: 30)]
        #endif
        public ElementType itemType = ElementType.Separator;
        
        /// <summary> Original Path. </summary>
        #if ODIN_INSPECTOR
        [ShowIf(condition: nameof(itemType), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
        [ValueDropdown(valuesGetter: nameof(MenuOptions))]
        [OnValueChanged(nameof(SetDefaultCustomOnOriginalChange))]
        #endif
        [field: SerializeField]
        public String Original { get; private set; }
        
        /// <summary> Customized Path. </summary>
        #if ODIN_INSPECTOR
        [ShowIf(condition: nameof(itemType), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
        #endif
        [field: SerializeField]
        public String Custom { get; private set; }

        protected abstract String MenuPath { get; }
        
        /// <summary> Gets a tree-view of all the submenus of "<see cref="MenuPath"/>". </summary>
        /// <returns> A tree-view of all the submenus of "<see cref="MenuPath"/>". </returns>
        private String[] MenuOptions => Unsupported.GetSubmenus(menuPath: MenuPath);

        #endregion

        #region Methods

        private void SetDefaultCustomOnOriginalChange()
        {
            if (Custom.NotNullOrEmpty()) return;
            Custom = Original;
        }

        #endregion
    }
}
