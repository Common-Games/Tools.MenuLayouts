using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
#endif

namespace CGTK.Tools.CustomizableMenus
{
    public abstract partial class MenuItem
    {
        //[CustomEditor(inspectedType: typeof(MenuItem))]
        //private class MenuItemEditor : Editor
        
        /*
        #if ODIN_INSPECTOR
        public class CustomStructDrawer : OdinValueDrawer<MenuItem>
        {
            private MenuItem InspectedMenuItem => ValueEntry.SmartValue;

            private InspectorProperty elementType;
            private InspectorProperty originalPath, overridePath;

            protected override void Initialize()
            {
                elementType = this.Property.Children[name: nameof(InspectedMenuItem.elementType)];
                originalPath = this.Property.Children[name: nameof(InspectedMenuItem.originalPath)];
                overridePath = this.Property.Children[name: nameof(InspectedMenuItem.overridePath)];
            }

            protected override void DrawPropertyLayout(GUIContent label)
            {
                Rect __rect = EditorGUILayout.GetControlRect();

                // In Odin, labels are optional and can be null, so we have to account for that.
                if (label != null)
                {
                    __rect = EditorGUI.PrefixLabel(__rect, label);
                }
                
                GUIHelper.PushLabelWidth(labelWidth: 75);
                //elementType.ValueEntry.WeakSmartValue = SirenixEditorFields.TextField(__rect.Split(0, 2), "Text", (string)elementTy)
                originalPath.ValueEntry.WeakSmartValue = SirenixEditorFields.TextField(rect: __rect.Split(0, 2), label: "Text", value: (string) originalPath.ValueEntry.WeakSmartValue);
                overridePath.ValueEntry.WeakSmartValue = SirenixEditorFields.TextField(rect: __rect.Split(1, 2), label: "Number", value: (string) overridePath.ValueEntry.WeakSmartValue);
                GUIHelper.PopLabelWidth();
            }
        }
        #endif
        */
        
        /*
        [CustomPropertyDrawer(type: typeof(MenuItem), useForChildren: true)]
        private class MenuItemDrawer : PropertyDrawer
        {
            //private MenuItem InspectedMenuItem { get; set; }

            public override VisualElement CreatePropertyGUI(SerializedProperty property)
            {
                base.CreatePropertyGUI(property);
                
                // Create property container element.
                VisualElement container = new VisualElement();

                // Create property fields.
                PropertyField elementType    = new PropertyField(property.FindPropertyRelative("elementType"));
                PropertyField originalPath   = new PropertyField(property.FindPropertyRelative("originalPath"));
                PropertyField overridePath   = new PropertyField(property.FindPropertyRelative("overridePath"), label: "Fancy Name");

                // Add fields to the container.
                container.Add(elementType);
                container.Add(originalPath);
                container.Add(overridePath);

                return container;
            }
        }
        */
        
        /*
        [CustomPropertyDrawer(type: typeof(MenuItem), useForChildren: true)]
        private class MenuItemDrawer : PropertyDrawer
        {
            //private MenuItem InspectedMenuItem { get; set; }

            public override VisualElement CreatePropertyGUI(SerializedProperty property)
            {
                base.CreatePropertyGUI(property);
                
                // Create property container element.
                VisualElement container = new VisualElement();

                // Create property fields.
                PropertyField elementType    = new PropertyField(property.FindPropertyRelative(nameof(elementType)));
                PropertyField originalPath   = new PropertyField(property.FindPropertyRelative(nameof(originalPath)));
                PropertyField overridePath   = new PropertyField(property.FindPropertyRelative(nameof(overridePath)), label: "Fancy Name");

                // Add fields to the container.
                container.Add(elementType);
                container.Add(originalPath);
                container.Add(overridePath);

                return container;
            }
        }
        */
    }
}
