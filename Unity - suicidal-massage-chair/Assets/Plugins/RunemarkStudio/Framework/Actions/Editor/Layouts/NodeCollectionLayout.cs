namespace Runemark.VisualEditor
{
    using UnityEditor;
    using Runemark.Common;

    public class NodeCollectionLayout : DefaultLayout 
	{
		public NodeCollectionLayout(Node node) : base(node){}	
	}


	public class NodeCollectionInspector : NodeInspector
	{
        protected override void OnHeaderGUI()
        {
            INodeCollection myTarget = (INodeCollection)target;
            Node node = (Node)myTarget;
            if (node.Variables.Count() > 0)
            {
                Variable variable = node.Variables.GetByID(myTarget.SelectedVariableID);
                if (variable != null)
                {
                    RunemarkGUI.inspectorTitle.Draw(variable.Name + " (Local Variable)", "");
                    return;
                }
            }
            base.OnHeaderGUI();     
        }


        public override void OnInspectorGUI()
		{

			INodeCollection myTarget = (INodeCollection)target;
			Node node = (Node)myTarget;
            if (node.Variables.Count() == 0) return;
		
			Variable variable = node.Variables.GetByID(myTarget.SelectedVariableID);

            VariableEditor.OnInspectorGUI(variable);			
		}

	}


	[CustomEditor(typeof(FunctionGraph), true)]
	public class FunctionGraphInspector : NodeCollectionInspector {}

	[CustomEditor(typeof(MacroGraph))]
	public class MacroGraphInspector : NodeCollectionInspector {}
}
