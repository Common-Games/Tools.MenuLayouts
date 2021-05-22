using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    public abstract class MenuItem
    {
        public enum ElementType
        {
            None,
            MenuPath,
            Separator,
        }
        
        #region Fields

        public ElementType elementType = ElementType.None;

        public string originalPath, overridePath;
        
        protected abstract string MenuPath { get; }

        #endregion
    }
}
