using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Object = System.Object;

namespace CGTK.Tools.CustomizableMenus
{
    using I32 = Int32;

    public abstract class MenuLayout<T_Item> : ScriptableObject, IEnumerator, IEnumerable
        where T_Item : MenuItem 
    {
        #region Fields
        
        #if ODIN_INSPECTOR
        [TableList(DrawScrollView = true, MinScrollViewHeight = 100, MaxScrollViewHeight = 1000)]
        #endif
        public List<T_Item> items = new List<T_Item>();

        #endregion

        #region Indexer

        [PublicAPI]
        public T_Item this[I32 index]
        {
            get => ((index >= 0) && (index <= Length - 1)) ? items[index] : null;
            set => items[index] = value;
        }
        
        #endregion

        #region Methods

        public I32 Length => items.Count; 

        public I32 CurrentIndex { get; private set; }= -1;
        public Boolean MoveNext() => ((CurrentIndex += 1) < items.Count);
        public   Object Current  => items[index: CurrentIndex];
        internal T_Item Previous => items[index: CurrentIndex - 1];
        internal T_Item Next     => items[index: CurrentIndex + 1];
        //Conflicts with Unity's Reset function, but that shouldn't matter too much. If it does I'll find out soon enough.
        //If it does I could make the Enumerator a nested class.
        public void Reset() => CurrentIndex = 0;

        public IEnumerator GetEnumerator() => this;

        #endregion
    }
}
