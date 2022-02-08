using System;
using System.Collections.Generic;

using UnityEngine;

using JetBrains.Annotations;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities.Editor;
#endif

#if ODIN_INSPECTOR
using ScriptableObject = Sirenix.OdinInspector.SerializedScriptableObject;
#else
using ScriptableObject = UnityEngine.ScriptableObject;
#endif

namespace CGTK.Tools.CustomizableMenus
{
    using I32 = Int32;

    [PublicAPI]
    internal abstract class MenuLayout<T_Item> : ScriptableObject //, ISerializationCallbackReceiver 
		where T_Item : MenuItem 
    {
        #region Fields
        
		#if ODIN_INSPECTOR
		[field: ListDrawerSettings(ShowItemCount = false, OnTitleBarGUI = nameof(DrawAddFolderButton))]
		[field: OdinSerialize]
		#endif
		public List<Element> Tree = new List<Element>(); //{ get; private set; } = new List<Element>();

		#if ODIN_INSPECTOR
		private void DrawAddFolderButton()
		{
			if (SirenixEditorGUI.ToolbarButton(icon: EditorIcons.Folder))
			{
				Tree.Add(item: new Element(isGroup: true));
			}
		}
		#endif
		
		//[Serializable]
        public class Element
        {
        	#region Fields

        	public Boolean IsGroup { get; set; }

        	private Boolean _showName = true;

        	private Boolean ShowGroupName => (IsGroup && _showName); 
        	
        	#if ODIN_INSPECTOR
        	[field: ShowIf(condition: nameof(ShowGroupName)), BoxGroup]
        	[field: HideLabel]
        	[field: CustomContextMenu(menuItem: "Hide GroupName", action: nameof(HideName))]
        	#endif
			[field: SerializeField]
        	public String GroupName { get; private set; }

        	#if ODIN_INSPECTOR
            [field: ShowIf(condition: nameof(IsGroup)), BoxGroup]
            [field: CustomContextMenu(menuItem: "Show GroupName", action: nameof(ShowName))]
            [field: CustomContextMenu(menuItem: "Hide GroupName", action: nameof(HideName))]
            [field: ListDrawerSettings(ShowItemCount = false, OnTitleBarGUI = nameof(DrawAddFolderButtonElement))]
            [field: LabelText(text: "$" + nameof(GroupName))]
            #endif
            public List<Element> Group = null; //{ get; private set; } = null;
        	
        	#if ODIN_INSPECTOR
        	[field: HideLabel]
        	[field: HideIf(condition: nameof(IsGroup))]
            #endif
            public T_Item Item = default;

			public MenuItem.ElementType ElementType => Item.Type; 
			
			public String OriginalPath => Item.Original;
			public String CustomPath   => Item.Custom;
        	
        	#endregion

        	#region Structors

            public Element() : this(isGroup: false)
            { }
            
        	public Element(Boolean isGroup = false)
        	{
        		this.IsGroup = isGroup;

        		if (isGroup)
        		{
        			Group = new List<Element>(capacity: 10);
        		}
                
                
        	}

        	#endregion

        	#region Methods
        	
        	#if ODIN_INSPECTOR
        	private void DrawAddFolderButtonElement()
        	{
        		if (SirenixEditorGUI.ToolbarButton(EditorIcons.Folder))
        		{
        			Group.Add(item: new Element(isGroup: true));
        		}
        	}
        	
        	private void ShowName() => _showName = true;
        	private void HideName() => _showName = false;
        	#endif

        	#endregion
        }

        #endregion
		
		#region Indexer
        
		[PublicAPI]
		public Element this[I32 index]
		{
			get => ((index >= 0) && (index <= Tree.Count)) ? Tree[index] : null;
			set => Tree[index] = value;
		}

		[PublicAPI] 
		public I32 Length => Tree.Count;

		#endregion
	}
}
