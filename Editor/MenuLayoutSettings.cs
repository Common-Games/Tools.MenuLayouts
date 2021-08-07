using System.Collections.Generic;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CGTK.Tools.CustomizableMenus
{
    internal sealed class Settings : ScriptableObject
    {
        public const string SETTINGS_PATH = Constants.EDITOR_FOLDER_PATH + "MyCustomSettings.asset";

        [SerializeField]
        internal int m_Number;

        [SerializeField]
        internal string m_SomeString;

        internal static Settings GetOrCreateSettings()
        {
            Settings __settings = AssetDatabase.LoadAssetAtPath<Settings>(SETTINGS_PATH);
            if (__settings != null) return __settings;
            
            __settings = CreateInstance<Settings>();
            __settings.m_Number = 42;
            __settings.m_SomeString = "The answer to the universe";
            AssetDatabase.CreateAsset(__settings, SETTINGS_PATH);
            AssetDatabase.SaveAssets();
            return __settings;
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
            SettingsProvider __provider = new(path: "Preferences/CGTK/Tools/Custom MenuLayouts", scopes: SettingsScope.User)
            {
                label = "Customizable Menus",
                
                // activateHandler is called when the user clicks on the Settings item in the Settings window.
                activateHandler = (searchContext, rootElement) =>
                {
                    SerializedObject __settings = Settings.GetSerializedSettings();

                    // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                    // isn't called because the SettingsProvider uses the UIElements drawing framework.
                    StyleSheet __styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Constants.STYLE_SHEET_PATH);
                    rootElement.styleSheets.Add(__styleSheet);

                    #region Title

                    

                    #endregion
                    
                    Label __title = new()
                    {
                        text = "Customizable Menus"
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
                        value = __settings.FindProperty(propertyPath: nameof(Settings.m_Number)).intValue
                    };
                    __integerField.AddToClassList(className: "property-value");
                    __visualElement.Add(__integerField);
                    
                    TextField __tf = new()
                    {
                        value = __settings.FindProperty(propertyPath: nameof(Settings.m_SomeString)).stringValue
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
                keywords = new HashSet<string>(collection: new[] { "Number", "Some String" })
            };

            return __provider;
        }
        
    }
}
