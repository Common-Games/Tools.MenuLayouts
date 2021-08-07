using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using JetBrains.Annotations;
using Sirenix.OdinInspector;

namespace CGTK.Tools.CustomizableMenus
{
    public abstract class MenuConfig<Item_T> : ScriptableObject
        where Item_T : MenuItem 
    {
        #region Fields
        
        [TableList]
        public List<Item_T> items = new List<Item_T>();

        private const String _SETTINGS_PATH = Constants.EDITOR_FOLDER_PATH + "MyCustomSettings.asset";
        
        /// <summary> Name of the Config file (without .asset extension) </summary>
        protected abstract String ConfigName { get; }
        protected String ConfigLocation => Constants.EDITOR_FOLDER_PATH + ConfigName + ".asset";

        #endregion

        #region Methods

        public Item_T this[Int32 index]
        {
            get => items[index];
            set => items[index] = value;
        }

        [PublicAPI]
        public MenuConfig<Item_T> GetOrCreateConfig
        {
            get
            {
                MenuConfig<Item_T>  __config = AssetDatabase.LoadAssetAtPath<MenuConfig<Item_T>>(assetPath: ConfigLocation);
                if (__config != null) return __config;
            
                __config = CreateInstance<MenuConfig<Item_T>>();

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
    }
}
