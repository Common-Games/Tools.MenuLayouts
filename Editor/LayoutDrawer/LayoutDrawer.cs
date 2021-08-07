using System;
using UnityEditor;
using UnityEngine;

using CGTK.Utilities.Extensions;

namespace CGTK.Tools.CustomizableMenus
{
	using static MenuItem.ElementType;

	//TODO: Separate Hierarchy / Project Layout Drawer?
	
	[InitializeOnLoad]
	internal static partial class LayoutDrawer
	{
		#region Fields

		//public static readonly bool UseCustomHierarchyMenu = true;
		public static readonly HierarchyMenuConfig CustomHierarchyMenuLayout = null;
		/// <summary>Menu for Hierarchy view.</summary>
		private static GenericMenu _hierarchyMenuDropdown;

		//public static readonly bool UseCustomProjectMenu = true;
		public static readonly ProjectMenuConfig CustomProjectMenuLayout = null;
		/// <summary>Menu for Project view.</summary>
		private static GenericMenu _projectMenuDropdown; // = new();
		
		#endregion

		#region Structors
		
		static LayoutDrawer()
		{
			SetupProjectMenu();
			SetupHierarchyMenu();
			
			EditorApplication.projectWindowItemOnGUI   += OnProjectGUI;
			EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
		}

		#endregion

		#region Methods
		
		private static void SetupHierarchyMenu()
		{
			if (CustomHierarchyMenuLayout == null) return;

			CreateGenericMenu(layout: CustomHierarchyMenuLayout, menu: out _hierarchyMenuDropdown);
		}
		
		private static void SetupProjectMenu()
		{
			if (CustomProjectMenuLayout == null) return;

			CreateGenericMenu(layout: CustomProjectMenuLayout, menu: out _projectMenuDropdown);
		}

		private static void OnHierarchyGUI(Int32 instanceID, Rect selectionRect)
		{
			if(CustomProjectMenuLayout == null || _hierarchyMenuDropdown == null) return;
			
			Event __currentEvent = Event.current;
			
			if(__currentEvent.type == EventType.ContextClick)
			{
				_hierarchyMenuDropdown.ShowAsContext();
				__currentEvent.Use();	
			}
		}

		private static void OnProjectGUI(String guid, Rect selectionRect)
		{
			if(CustomProjectMenuLayout == null || _projectMenuDropdown == null) return;
			
			Event __currentEvent = Event.current;
			
			if(__currentEvent.type == EventType.ContextClick)
			{
				_projectMenuDropdown.ShowAsContext();
				__currentEvent.Use();	
			}
		}
		
		/// <summary> Creates a menu tree from MenuLayout list. </summary>
		private static void CreateGenericMenu<T>(in MenuConfig<T> layout, out GenericMenu menu)
			where T : MenuItem
        {
			if (layout == null)
			{
				menu = null;
				return;
			}
			
			menu = new GenericMenu();
			//if(layout == null) return;
			
            Int32 __itemCount = layout.items.Count;
			
			T __prevItem = null;

			for(Int32 __index = 0; __index < __itemCount; ++__index) //TODO: ++
			{
				T __currentItem = layout[__index];

				switch(__currentItem.itemType)
				{
					//case MenuItem.ElementType.None:
					case Separator when (__prevItem == null): // If there is no previous item, skip.
					case Separator when (__prevItem.Custom.IsNullOrEmpty()): // If previous item is null or empty, skip.
					case Separator when (__index == 0 || __index == __itemCount-1): // If the item is first or last, skip.
						continue;
					
					case Separator:
					{
						T __nextItem = layout[index: __index+1];

						// Double separators are skipped.
						if(__nextItem.itemType == Separator) continue;

						(String __matchingPart, _, _) = __prevItem.Original.SplitAtDeviation(__nextItem.Custom);

						if(__matchingPart == null) continue;

						if(__matchingPart != "")
						{
							Int32 __lastIndexOfSlash = __matchingPart.LastIndexOf('/');

							if(__lastIndexOfSlash != -1)
							{
								__matchingPart = __matchingPart.Substring(startIndex: 0, length: __lastIndexOfSlash + 1);
							}
						}
						
						menu.AddSeparator(path: __matchingPart);
					
						break;
					}
					case Path:
					{
						menu.AddItem(
							content: new GUIContent(__currentItem.Custom), 
							on: false, 
							func: (command => EditorApplication.ExecuteMenuItem(menuItemPath: command as String)), 
							userData: __currentItem.Original);

						__prevItem = __currentItem;
						break;
					}
					default:
						throw new System.ArgumentOutOfRangeException();
				}
			}
		}
				
		#endregion
	
	}
}
