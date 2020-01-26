namespace Runemark.DialogueSystem
{
    using Runemark.Common;
    using UnityEditor;

    [CustomEditor(typeof(BasicSaveLoad), true)]
    public class BasicSaveLoadEditor : RunemarkEditor
    {
        void OnEnable()
        {
            FindProperty("mode");
            FindProperty("FileName");
            FindProperty("SaveOnExit");
            FindProperty("LoadOnStart");
        }

        public override void OnInspectorGUI()
        {
            BasicSaveLoad myTarget = (BasicSaveLoad)target;

            RunemarkGUI.inspectorTitle.Draw("Basic Save & Load",
                "This component will save and load your saveable Local and Global variables.");

            serializedObject.Update();
            EditorGUIExtension.SimpleBox("Settings", 5, "", delegate () 
            {
                DrawPropertyField("mode", "Save Mode");

                if (myTarget.mode == BasicSaveLoad.Mode.File)
                {
                    DrawPropertyField("FileName");     
                }

                EditorGUILayout.Space();

                DrawPropertyField("SaveOnExit");
                DrawPropertyField("LoadOnStart");
            });
            serializedObject.ApplyModifiedProperties();
        }
    }
}