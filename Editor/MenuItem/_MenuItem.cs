using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

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

        [TableColumnWidth(width: 30)]
        //[LabelText("Type")]
        public ElementType type = ElementType.Separator;
        
        /// <summary> Original Path. </summary>
        [ShowIf(nameof(type), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
        [OnValueChanged(nameof(SetDefaultCustomOnOriginalChange))]
        [ValueDropdown(valuesGetter: nameof(MenuOptions))]
        public string original;
        
        /// <summary> Customized Path. </summary>
        [ShowIf(nameof(type), ElementType.Path)]
        [TableColumnWidth(width: 60, resizable: true)]
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
