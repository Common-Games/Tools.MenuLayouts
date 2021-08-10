using System;
using System.Runtime.CompilerServices;

using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.Utilities.Editor;
#endif

using CGTK.Utilities.Shared;
using JetBrains.Annotations;

namespace CGTK.Tools.CustomizableMenus
{
    /// <summary>
    /// This class will act as a manager for the <see cref="Settings"/> singleton.
    /// </summary>
    internal static class SettingsManager
    {
        private static Settings _internalInstance;
        internal static Settings Instance => _internalInstance ??= new Settings(PackageConstants.PACKAGE_NAME);
        public static void Save() => Instance.Save();

        [SettingsProvider]
        private static SettingsProvider Create() //TODO: TryGet?
        {
            UserSettingsProvider __provider = new UserSettingsProvider(path: "Preferences/CGTK/Tools/Custom Menu Layouts",
                settings: Instance,
                assemblies: new [] { typeof(SettingsManager).Assembly });
            
            return __provider;
        }
    }
    
    internal sealed class Setting<T> : UserSetting<T>
    {
        public Setting(T value, [CallerMemberName] String key = "", in SettingsScope scope = SettingsScope.User)
            : base(settings: SettingsManager.Instance, key: key, value: value, scope: scope)
        {
        }
    }
    
    [PublicAPI]
    internal class Preferences : EditorWindow
    {
        #region Fields
        
        [UserSetting] 
        private static readonly Setting<MenuLayoutHierarchy> HierarchyMenuLayout = new Setting<MenuLayoutHierarchy>(value: null);
        [UserSetting] 
        private static readonly Setting<MenuLayoutProject>   ProjectMenuLayout   = new Setting<MenuLayoutProject>(value: null);

        public static MenuLayoutHierarchy CustomHierarchyMenuLayout
        {
            get => HierarchyMenuLayout.value;
            
            private 
            set => HierarchyMenuLayout.value = value;
        } 
        public static MenuLayoutProject CustomProjectMenuLayout
        {
            get => ProjectMenuLayout.value;

            private
            set => ProjectMenuLayout.value = value;
        }

        #endregion

        [UserSettingBlock(category: "Menu Layouts")]
        private static void OnSearchGUI(String searchContext)
        {
            EditorGUI.BeginChangeCheck();
            { 
                DrawHierarchyMenuLayoutSelector(searchContext);
                DrawProjectMenuLayoutSelector(searchContext);
            }
            if (EditorGUI.EndChangeCheck())
            {
                SettingsManager.Save();
            }
        }

        private static void DrawHierarchyMenuLayoutSelector(in String searchContext)
        {
            GUIStyle __buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = 18,
                fixedWidth  = 18,
                padding     = new RectOffset(left: 0, right: 0, top: 0, bottom: 0)
            }; //Somehow get Errors when using a static version of this.... So unfortunately we have to do this. I could send them as a parameter, but don't want to.
            
            EditorGUILayout.BeginHorizontal();
            {
                #if ODIN_INSPECTOR
                GUILayout.Label(text: "HierarchyMenu Layout", options: GUILayout.Width(145));

                CustomHierarchyMenuLayout = SirenixEditorFields.UnityObjectField(
                    value: CustomHierarchyMenuLayout,
                    objectType: typeof(MenuLayoutHierarchy), 
                    allowSceneObjects: false) as MenuLayoutHierarchy;
                #else
                CustomHierarchyMenuLayout = EditorGUILayout.ObjectField(
                    label: "HierarchyMenu Layout", 
                    obj: CustomHierarchyMenuLayout, 
                    objType: typeof(MenuLayoutHierarchy), 
                    allowSceneObjects: false) as MenuLayoutHierarchy;
                #endif
                
                if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Toolbar Plus", text: "Create"), __buttonStyle))
                {
                    CustomHierarchyMenuLayout = ScriptableObjectCreator.Create<MenuLayoutHierarchy>(directory: "Assets");
                }   
                    
                if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Refresh", text: "Reset"), __buttonStyle))
                {
                    CustomHierarchyMenuLayout = null;
                }
            }
            EditorGUILayout.EndHorizontal();

            LayoutDrawer.UpdateHierarchyMenuLayout();
        }
        
        private static void DrawProjectMenuLayoutSelector(in String searchContext)
        {
            GUIStyle __buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = 18,
                fixedWidth  = 18,
                padding     = new RectOffset(left: 0, right: 0, top: 0, bottom: 0)
            }; //Somehow get Errors when using a static version of this.... So unfortunately we have to do this. I could send them as a parameter, but don't want to.
            
            EditorGUILayout.BeginHorizontal();
            {
                #if ODIN_INSPECTOR
                GUILayout.Label(text: "ProjectMenu Layout", options: GUILayout.Width(145));
                    
                CustomProjectMenuLayout = SirenixEditorFields.UnityObjectField(
                    value: CustomProjectMenuLayout,
                    objectType: typeof(MenuLayoutProject), 
                    allowSceneObjects: false) as MenuLayoutProject;
                #else
                CustomProjectMenuLayout = EditorGUILayout.ObjectField(
                    label: "ProjectMenu Layout", 
                    obj: CustomProjectMenuLayout, 
                    objType: typeof(MenuLayoutProject), 
                    allowSceneObjects: false) as MenuLayoutProject;
                #endif
                
                if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Toolbar Plus", text: "Create"), __buttonStyle))
                {
                    CustomProjectMenuLayout = ScriptableObjectCreator.Create<MenuLayoutProject>(directory: "Assets");
                }   
                    
                if (GUILayout.Button(content: EditorGUIUtility.IconContent(name: "d_Refresh", text: "Reset"), __buttonStyle))
                {
                    CustomProjectMenuLayout = null;
                }
            }
            EditorGUILayout.EndHorizontal();

            LayoutDrawer.UpdateProjectMenuLayout();
        }
    }
}
