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

    public abstract class MenuConfig<T_Item> : ScriptableObject, IEnumerator, IEnumerable
        where T_Item : MenuItem 
    {
        #region Fields
        
        #if ODIN_INSPECTOR
        [TableList]
        #endif
        public List<T_Item> items = new List<T_Item>();

        //private const String _SETTINGS_PATH = Constants.EDITOR_FOLDER_PATH + "MyCustomSettings.asset";
        
        /// <summary> Name of the Config file (without .asset extension) </summary>
        protected abstract String ConfigName { get; }
        protected String ConfigLocation => Constants.EDITOR_FOLDER_PATH + ConfigName + ".asset";

        #endregion

        #region Indexer

        [PublicAPI]
        public T_Item this[I32 index]
        {
            get => items[index];
            set => items[index] = value;
        }
        
        #endregion

        #region Methods

        [PublicAPI]
        public MenuConfig<T_Item> GetOrCreateConfig
        {
            get
            {
                MenuConfig<T_Item>  __config = AssetDatabase.LoadAssetAtPath<MenuConfig<T_Item>>(assetPath: ConfigLocation);
                if (__config != null) return __config;
            
                __config = CreateInstance<MenuConfig<T_Item>>();

                AssetDatabase.CreateAsset(asset: __config, path: ConfigLocation);
                AssetDatabase.SaveAssets();
                
                return __config;   
            }
        }
        
        internal SerializedObject GetSerializedConfig()
        {
            return new SerializedObject(obj: GetOrCreateConfig);
        }

        #endregion

        #region IEnumerator/IEnumerable Impl

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
