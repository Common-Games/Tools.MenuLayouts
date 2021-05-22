using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CGTK.Tools.CustomizableMenus
{
    // Create a new type of Settings Asset.
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
            return new SerializedObject(obj: GetOrCreateSettings());
        }
    }
    
    // Register a SettingsProvider using UIElements for the drawing framework:
    static class SettingsRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
            SettingsProvider __provider = new SettingsProvider(path: "Preferences/CGTK/Tools/Customizable Menus", SettingsScope.User)
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
                    
                    Label __title = new Label
                    {
                        text = "Customizable Menus"
                    };
                    
                    __title.AddToClassList(className: "title");
                    rootElement.Add(__title);

                    VisualElement __properties = new VisualElement
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Column
                        }
                    };
                    __properties.AddToClassList(className: "property-list");
                    rootElement.Add(__properties);

                    IntegerField __integerField = new IntegerField
                    {
                        value = __settings.FindProperty(propertyPath: nameof(Settings.m_Number)).intValue
                    };
                    __integerField.AddToClassList(className: "property-value");
                    __properties.Add(__integerField);
                    
                    TextField __tf = new TextField
                    {
                        value = __settings.FindProperty(propertyPath: nameof(Settings.m_SomeString)).stringValue
                    };
                    __tf.AddToClassList(className: "property-value");
                    __properties.Add(__tf);
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(collection: new[] { "Number", "Some String" })
            };

            return __provider;
        }
    }
}
