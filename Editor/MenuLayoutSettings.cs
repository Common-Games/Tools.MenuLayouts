using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CGTK.Tools.CustomizableMenus
{
    internal sealed class MenuLayoutSettings : ScriptableObject
    {
        private const String _SETTINGS_PATH = Constants.EDITOR_FOLDER_PATH + nameof(MenuLayoutSettings) + ".asset";

        [SerializeField]
        internal Int32 m_Number;

        [SerializeField]
        internal String m_SomeString;

        internal static MenuLayoutSettings GetOrCreateSettings()
        {
            MenuLayoutSettings __menuLayoutSettings = AssetDatabase.LoadAssetAtPath<MenuLayoutSettings>(_SETTINGS_PATH);
            if (__menuLayoutSettings != null) return __menuLayoutSettings;
            
            __menuLayoutSettings = CreateInstance<MenuLayoutSettings>();
            __menuLayoutSettings.m_Number = 42;
            __menuLayoutSettings.m_SomeString = "The answer to the universe";
            AssetDatabase.CreateAsset(__menuLayoutSettings, _SETTINGS_PATH);
            AssetDatabase.SaveAssets();
            return __menuLayoutSettings;
        }
        
        internal static SerializedObject GetSerializedSettings()
        {
            return new(obj: GetOrCreateSettings());
        }
    }

    [InitializeOnLoad]
    internal static class SettingsRegistry
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
            SettingsProvider __provider = new(path: "Preferences/CGTK/Tools/Custom Menu Layouts", scopes: SettingsScope.User)
            {
                label = "Custom Menu Layouts",
                
                // activateHandler is called when the user clicks on the Settings item in the Settings window.
                activateHandler = (searchContext, rootElement) =>
                {
                    SerializedObject __settings = MenuLayoutSettings.GetSerializedSettings();

                    // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                    // isn't called because the SettingsProvider uses the UIElements drawing framework.
                    StyleSheet __styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Constants.STYLE_SHEET_PATH);
                    rootElement.styleSheets.Add(__styleSheet);

                    #region Title

                    

                    #endregion
                    
                    Label __title = new()
                    {
                        text = "Custom Menu Layouts"
                    };
                    
                    __title.AddToClassList(className: "title");
                    rootElement.Add(__title);

                    VisualElement __visualElement = new()
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Column
                        }
                    };

                    #region HierarchyLayout

                    #region Label

                    //Label uxmlLabel = __visualElement.Q<Label>("the-uxml-label");
                    //__visualElement.Add(uxmlLabel);
                    
                    Label __label = new("C# Label");
                    __label.AddToClassList("some-styled-label");
                    __visualElement.Add(__label);

                    #endregion
                    

                    #endregion

                    __visualElement.AddToClassList(className: "property-list");
                    rootElement.Add(__visualElement);

                    IntegerField __integerField = new()
                    {
                        value = __settings.FindProperty(propertyPath: nameof(MenuLayoutSettings.m_Number)).intValue
                    };
                    __integerField.AddToClassList(className: "property-value");
                    __visualElement.Add(__integerField);
                    
                    TextField __tf = new()
                    {
                        value = __settings.FindProperty(propertyPath: nameof(MenuLayoutSettings.m_SomeString)).stringValue
                    };
                    __tf.AddToClassList(className: "property-value");
                    __visualElement.Add(__tf);

                    void __Action()
                    {
                        __visualElement.Query<Button>().ForEach(button =>
                        {
                            button.text = button.text.EndsWith(value: "Button") ? "Button (Clicked)" : "Button";
                        });
                    }

                    Button __newHierarchyLayoutButton = new(__Action) { text = "New" };
                    __newHierarchyLayoutButton.AddToClassList(className: "some-styled-button");
                    __visualElement.Add(__newHierarchyLayoutButton);
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<String>(collection: new[] { "Number", "Some String" })
            };

            return __provider;
        }
        
    }
}
