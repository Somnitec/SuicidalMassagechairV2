namespace Runemark.VisualEditor
{
    using UnityEditor;
    using Runemark.VisualEditor.Utility;
    using Runemark.VisualEditor.Actions;


    [CustomNodeLayout(typeof(Wait), true)]
	public class WaitLayout : DefaultLayout
	{ 
		public WaitLayout (Node node) : base(node)
		{
			headerColor = BuiltInColors.DarkDefault;
		} 
	} 


	[CustomEditor(typeof(Wait))]
	public class WaitInspector : NodeInspector
	{
		protected override void onGUI()
		{
			Wait myTarget = (Wait)target;
		}
	}
} 
