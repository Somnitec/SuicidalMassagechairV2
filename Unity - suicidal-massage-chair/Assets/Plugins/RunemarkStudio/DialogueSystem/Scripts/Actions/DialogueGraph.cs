namespace Runemark.DialogueSystem
{
    using Runemark.VisualEditor;
    using UnityEngine;

    [System.Serializable]
    public class DialogueGraph : FunctionGraph
	{
        public Translator translator = new Translator();

    	protected override void OnEnter()
		{
			Owner.CallEvent("OnOpen");
		}

        public override Node Copy(bool copyConnections = false)
        {
            DialogueGraph newNode = (DialogueGraph)base.Copy(copyConnections);
            newNode.translator = translator;
            return newNode;
        }
    }
}