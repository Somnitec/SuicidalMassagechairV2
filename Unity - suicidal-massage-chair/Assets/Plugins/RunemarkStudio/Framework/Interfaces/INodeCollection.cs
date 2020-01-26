namespace Runemark.VisualEditor
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface INodeCollection
	{
		#region EditorStuff
		bool HasChanges { get; set; }

		Vector2 ZoomOffset { get; set; }
		string SelectedVariableID { get; set; }
		List<CommentBox> Bookmarks { get; }
		#endregion

		NodeCollection Nodes { get; }
		FunctionGraph Root { get; }

        System.Type GetType();
	}
}