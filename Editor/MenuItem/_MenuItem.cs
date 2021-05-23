using System;

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
        public ElementType type = ElementType.Separator;
        
        /// <summary> Original Path. </summary>
        #if ODIN_INSPECTOR
        [ShowIf(nameof(type), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
        [ValueDropdown(valuesGetter: nameof(MenuOptions))]
        [OnValueChanged(nameof(SetDefaultCustomOnOriginalChange))]
        #endif
        public string original;
        
        /// <summary> Customized Path. </summary>
        #if ODIN_INSPECTOR
        [ShowIf(nameof(type), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
        #endif
        public string custom;

        protected abstract string MenuPath { get; }
        
        /// <summary> Gets a tree-view of all the submenus of "<see cref="MenuPath"/>". </summary>
        /// <returns> A tree-view of all the submenus of "<see cref="MenuPath"/>". </returns>
        private string[] MenuOptions => Unsupported.GetSubmenus(menuPath: MenuPath);

        #endregion

        #region Methods

        private void SetDefaultCustomOnOriginalChange()
        {
            if (!string.IsNullOrEmpty(custom)) return;
            custom = original;
        }

        #endregion
    }
}
