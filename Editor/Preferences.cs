//#if UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using JetBrains.Annotations;

#if ODIN_INSPECTOR
using Sirenix.Utilities.Editor;
#endif

using CGTK.Utilities.Shared;

namespace CGTK.Tools.CustomizableMenus
{
    [Serializable]
    internal static class Preferences
    {
        public static MenuLayoutHierarchy CustomHierarchyMenuLayout { get; internal set; }
        public static MenuLayoutProject   CustomProjectMenuLayout   { get; internal set; }
    }
    
    public sealed class ScriptTemplatesSettingsProvider : SettingsProvider
    {
        private static readonly GUIStyle ButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fixedHeight = 18,
            fixedWidth  = 18,
            padding = new RectOffset(left: 0, right: 0, top: 0, bottom: 0)
        };

        [PublicAPI]
        public ScriptTemplatesSettingsProvider(in String path, in SettingsScope scopes, in IEnumerable<String> keywords = null) : base(path, scopes, keywords)
        { }

        public override void OnGUI(String searchContext)
        {
            __DrawHierarchyMenuLayoutSelector();
            __DrawProjectMenuLayoutSelector();

            static void __DrawHierarchyMenuLayoutSelector()
            {
                EditorGUILayout.BeginHorizontal();
                {
                    #if ODIN_INSPECTOR
                    Preferences.CustomHierarchyMenuLayout = SirenixEditorFields.UnityObjectField(
                        label: "HierarchyMenu Layout", 
                        value: Preferences.CustomHierarchyMenuLayout,
                        objectType: typeof(MenuLayoutHierarchy), 
                        allowSceneObjects: false) as MenuLayoutHierarchy;
                    #else
                    Preferences.CustomHierarchyMenuLayout = EditorGUILayout.ObjectField(
                        label: "HierarchyMenu Layout", 
                        obj: Preferences.CustomHierarchyMenuLayout, 
                        objType: typeof(MenuLayoutHierarchy), 
                        allowSceneObjects: false) as MenuLayoutHierarchy;
                    #endif

                    if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Toolbar Plus", text: "Create"), ButtonStyle))
                    {
                        Preferences.CustomHierarchyMenuLayout = ScriptableObjectCreator.Create<MenuLayoutHierarchy>(directory: "Assets");
                    }   
                    
                    if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Refresh", text: "Reset"), ButtonStyle))
                    {
                        Preferences.CustomHierarchyMenuLayout = null;
                    }  
                }
                EditorGUILayout.EndHorizontal();

                LayoutDrawer.UpdateHierarchyMenuLayout();
            }

            static void __DrawProjectMenuLayoutSelector()
            {
                EditorGUILayout.BeginHorizontal();
                {
                    #if ODIN_INSPECTOR
                    Preferences.CustomProjectMenuLayout = SirenixEditorFields.UnityObjectField(
                        label: "ProjectMenu Layout", 
                        value: Preferences.CustomProjectMenuLayout,
                        objectType: typeof(MenuLayoutProject), 
                        allowSceneObjects: false) as MenuLayoutProject;
                    #else
                    Preferences.CustomProjectMenuLayout = EditorGUILayout.ObjectField(
                        label: "ProjectMenu Layout", 
                        obj: Preferences.CustomProjectMenuLayout, 
                        objType: typeof(MenuLayoutProject), 
                        allowSceneObjects: false) as MenuLayoutProject;
                    #endif
                    
                    if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Toolbar Plus", text: "Create"), ButtonStyle))
                    {
                        Preferences.CustomProjectMenuLayout = ScriptableObjectCreator.Create<MenuLayoutProject>(directory: "Assets");
                    }   
                    
                    if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Refresh", text: "Reset"), ButtonStyle))
                    {
                        Preferences.CustomProjectMenuLayout = null;
                    }  
                }
                EditorGUILayout.EndHorizontal();

                LayoutDrawer.UpdateProjectMenuLayout();
            }
        }

        [SettingsProvider]
        public static SettingsProvider Create() 
            => new ScriptTemplatesSettingsProvider(path: PackageConstants.PREFERENCE_PATH, scopes: SettingsScope.User);
    }
}
//#endif