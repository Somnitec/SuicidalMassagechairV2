namespace Runemark.VisualEditor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using Runemark.VisualEditor.Utility;
    using UnityEditorInternal;
    using Runemark.Common;

    public class BookmarkReorderableList : ReorderableListGUI
	{
        public delegate void OnBookmarkAction(Node node);
        public OnBookmarkAction onBookmarkSelected;


        public override string Title { get { return "Bookmarks"; } }
        protected override bool displayRemoveButton
        {
            get
            {
                return false;
            }
        }
        protected override bool displayAddButton
        {
            get
            {
                return false;
            }
        }

        public override bool UseAddDropdown
        {
            get
            {
                return false;
            }
        }

        protected List<CommentBox> list;


        public BookmarkReorderableList(List<CommentBox> list) : base (list)
		{
            this.list = list;           
        }

        public void Refresh(List<CommentBox> list)
        {
            Init(list);
        }

        protected override void drawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index >= list.Count) return;
            var element = list[index];        
            GUI.Label(new Rect(rect.x + 25, rect.y, rect.width - 30, 20), element.Name);
        }

        protected override void onSelectCallback(UnityEditorInternal.ReorderableList list)
        {
            if (onBookmarkSelected != null)
                onBookmarkSelected(this.list[list.index]);            
        }

        protected override void onAddCallback(ReorderableList list)
        {
           
        }
        protected override void onAddDropdownCallback(Rect buttonRect, ReorderableList list)
        {
            
        }
        protected override bool onCanRemoveCallback(ReorderableList list)
        {
            return false;
        }
        protected override void onRemoveCallback(ReorderableList list)
        {
            
        }
    }     
}