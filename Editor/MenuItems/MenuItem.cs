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
        [TableColumnWidth(30)]
        #endif
        [field: SerializeField]
        public ElementType Type { get; private set; } = ElementType.Separator;
        
        /// <summary> Original Path. </summary>
        #if ODIN_INSPECTOR
        [field: ShowIf(condition: nameof(Type), ElementType.Path)]
        [field: TableColumnWidth(60, resizable: true)]
        [field: ValueDropdown(valuesGetter: nameof(MenuOptions))]
        [field: OnValueChanged(action: nameof(SetDefaultCustomOnOriginalChange))]
        #endif
        [field: SerializeField]
        public String Original { get; private set; }
        
        /// <summary> Customized Path. </summary>
        #if ODIN_INSPECTOR
        [field: ShowIf(condition: nameof(Type), ElementType.Path)]
        [field: TableColumnWidth(60, resizable: true)]
        #endif
        [field: SerializeField]
        public String Custom { get; private set; }

        protected abstract String MenuPath { get; }
        
        /// <summary> Gets a tree-view of all the submenus of "<see cref="MenuPath"/>". </summary>
        /// <returns> A tree-view of all the submenus of "<see cref="MenuPath"/>". </returns>
        private String[] MenuOptions => Unsupported.GetSubmenus(MenuPath);

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
