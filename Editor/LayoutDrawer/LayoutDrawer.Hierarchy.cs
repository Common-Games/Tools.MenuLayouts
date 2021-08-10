using System;
using UnityEditor;
using UnityEngine;

namespace CGTK.Tools.CustomizableMenus
{
	using I32 = Int32;
	
	internal static partial class LayoutDrawer
	{
		#region Fields
		
		/// <summary>Menu for Hierarchy view.</summary>
		private static GenericMenu _hierarchyMenu;

		#endregion

		#region Methods

		public static void UpdateHierarchyMenuLayout()
		{
			if (Preferences.CustomHierarchyMenuLayout == null) return;

			CreateGenericMenu(layout: Preferences.CustomHierarchyMenuLayout, menu: out _hierarchyMenu);
		}

		private static void OnHierarchyGUI(I32 instanceID, Rect selectionRect)
		{
			if(Preferences.CustomHierarchyMenuLayout == null || _hierarchyMenu == null) return;
			
			Event __currentEvent = Event.current;

			if (__currentEvent.type != EventType.ContextClick) return;
			
			_hierarchyMenu.ShowAsContext();
			__currentEvent.Use();
		}

		#endregion
	
	}
}
