using System;
using UnityEditor;
using UnityEngine;

using CGTK.Utilities.Extensions;

namespace CGTK.Tools.CustomizableMenus
{
	using static MenuItem.ElementType;
	
	using I32 = Int32;
	
	[InitializeOnLoad]
	internal static partial class LayoutDrawer
	{
		static LayoutDrawer()
		{
			Debug.Log(message: "Started Layout Drawer!");
			
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

			//foreach (T __item in layout)
			for(I32 __index = 0; __index < layout.Length; __index++)
			{
				//I32 __index = layout.CurrentIndex;
				
				T __prev = layout[__index - 1];
				T __curr = layout[__index];
				T __next = layout[__index + 1];

				switch(__curr.Type)
				{
					case Separator when (__next == null || __next.Custom.IsNullOrEmpty()): // If there is no next item, skip.
					case Separator when (__prev == null || __prev.Custom.IsNullOrEmpty()): // If previous item is null or empty, skip.
						continue;

					case Separator:
					{
						// Double separators are skipped.
						if(__next.Type == Separator) continue;
						
						(String __matchingPart, _, _) = __prev.Original.SplitAtDeviation(__next.Custom);
						
						if(__matchingPart == null) continue;

						Boolean __isSubGroup = (__matchingPart != String.Empty);
						
						if(__isSubGroup)
						{
							I32 __lastIndexOfSlash = __matchingPart.LastIndexOf('/');

							if(__lastIndexOfSlash != -1)
							{
								__matchingPart = __matchingPart[..(__lastIndexOfSlash + 1)];
							}
						}
						
						menu.AddSeparator(path: __matchingPart);
					
						break;
					}
					case Path:
					{
						menu.AddItem(
							content: new GUIContent(__curr.Custom), 
							on: false, 
							func: (command => EditorApplication.ExecuteMenuItem(menuItemPath: command as String)), 
							userData: __curr.Original);

						//__prevItem = __item;
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}
