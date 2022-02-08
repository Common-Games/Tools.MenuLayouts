#if UNITY_EDITOR
using System;
using static System.IO.Path;

using UnityEditor;
using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
	using static MenuItem.ElementType;
	
	using I32 = Int32;
	
	[InitializeOnLoad]
	internal static partial class LayoutDrawer
	{
		static LayoutDrawer()
		{
			EditorApplication.projectWindowItemOnGUI   += OnProjectGUI;
			EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
		}

		/// <summary> Creates a menu tree from MenuLayout list. </summary>
		/// <remarks> It also dictates the rules of how to draw it, what do with double separators, grouping, that sort of thing. </remarks>
		private static void CreateGenericMenu<T>(in MenuLayout<T> layout, out GenericMenu menu)
			where T : MenuItem
        {
			if (layout == null) throw new ArgumentException(message: nameof(layout)); //TODO: Return null instead?

			menu = new GenericMenu();
			
			for (I32 __index = 0; __index < layout.Length; __index++) //TODO: Make MenuLayout IEnumerable
			{
				MenuLayout<T>.Element __element = layout[__index];
				
				if(__element == null) continue;
				if(__element.Equals(null)) continue;

				if (__element.IsGroup)
				{
					__Create(menu: menu, element: __element, group: __element.GroupName);	
				}
				else
				{
					if (__element.ElementType == Separator)
					{
						menu.AddSeparator(path: String.Empty);
					}
					else
					{
						menu.AddItem(
							content: new GUIContent(__element.CustomPath), 
							on: false, 
							func: (command => EditorApplication.ExecuteMenuItem(menuItemPath: command as String)), 
							userData: __element.OriginalPath);
					}
				}
			}

			static void __Create(GenericMenu menu, MenuLayout<T>.Element element, String group)
			{
				foreach (MenuLayout<T>.Element __element in element.Group)
				{
					if(__element == null) continue;
					if(__element.Equals(null)) continue;

					if (__element.IsGroup)
					{
						__Create(menu: menu, element: __element, @group: group + "/" + __element.GroupName);
					}
					else
					{
						if (__element.ElementType == Separator)
						{
							menu.AddSeparator(path: group + "/");
						}
						else
						{
							menu.AddItem(
								content: new GUIContent(text: group + "/" + __element.CustomPath), 
								@on: false, 
								func: (command => EditorApplication.ExecuteMenuItem(menuItemPath: command as String)), 
								userData: __element.OriginalPath);
						}
					}
				}
			}
		}
	}
}
#endif
