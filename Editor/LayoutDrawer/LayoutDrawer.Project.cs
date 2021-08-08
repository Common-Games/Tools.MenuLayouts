using System;

using UnityEditor;
using UnityEngine;

using JetBrains.Annotations;

namespace CGTK.Tools.CustomizableMenus
{
	using I32 = Int32;
	
	internal static partial class LayoutDrawer
	{
		#region Fields
		
		/// <summary>Menu for Project view.</summary>
		private static GenericMenu _projectMenu;
		
		#endregion

		#region Methods

		[PublicAPI]
		public static void UpdateProjectMenuLayout()
		{
			if (Preferences.CustomProjectMenuLayout == null) return;

			CreateGenericMenu(layout: Preferences.CustomProjectMenuLayout, menu: out _projectMenu);
			
			//Debug.Log(message: $"Project Menu = {_projectMenu}, ItemCount = {_projectMenu.GetItemCount()}");
		}

		private static void OnProjectGUI(String guid, Rect selectionRect)
		{
			if(Preferences.CustomProjectMenuLayout == null || _projectMenu == null) return;
			
			Event __currentEvent = Event.current;

			if (__currentEvent.type != EventType.ContextClick) return;
			
			_projectMenu.ShowAsContext();
			__currentEvent.Use();
		}

		#endregion
	
	}
}
