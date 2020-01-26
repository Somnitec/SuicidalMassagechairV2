namespace Runemark.DialogueSystem
{
    using UnityEngine;
    using UnityEditor;
    using Runemark.Common;
    using Runemark.VisualEditor.Actions;

    public static class DialogueSystemMenu 
	{
		// Create new graph
		[MenuItem("Assets/Create/Runemark/Dialogue System/New Dialogue", priority = 10)]
		static void CreateNewGraph() 
		{	
			var root = AssetCreator.CreateAsset<DialogueGraph>();
            string name = root.Name;
            root.Name = "DialogueRoot";
            root.EditorInit(null, Vector2.zero);
            root.Name = name;

            ConversationFlow c = new ConversationFlow();        
            SimpleEvent onOpen = AssetCreator.CreateAsset<SimpleEvent>(root);
			onOpen.Name = c.EVENT_NAME;
			onOpen.EditorInit(root, new Vector2(0, -50));
			onOpen.CanCopy = false;
			onOpen.CanDelete = false;
			root.Nodes.Add(onOpen);

            AmbientDialogue a = new AmbientDialogue();
            SimpleEvent onBark = AssetCreator.CreateAsset<SimpleEvent>(root);
			onBark.Name = a.EVENT_NAME;
			onBark.EditorInit(root, new Vector2(0, 50));
			onBark.CanCopy = false;
			onBark.CanDelete = false;
			root.Nodes.Add(onBark);
		}

        [MenuItem("Window/Runemark/Language Database", false, 500)]
        static void SelectLanguageDB()
        {
            Selection.activeObject = LanguageDatabase.Instance;
            EditorGUIUtility.PingObject(LanguageDatabase.Instance);
        }
    }
}