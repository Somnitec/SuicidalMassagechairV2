namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using UnityEditor;

    [CustomEditor(typeof(UIElement), true)]
    public class DialogueSystemUIElementEditor : RunemarkEditor
    {
        public override void OnInspectorGUI()
        {
            UIElement myTarget = (UIElement)target;

            var t = myTarget.GetType().ToString().Split('.');
            string title = t[t.Length-1].Replace("UIElement","");

            RunemarkGUI.inspectorTitle.Draw("Dialogue System - "+title, "");

            serializedObject.Update();
            onGUI();
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
        }

        protected virtual void onGUI(){ DrawDefaultInspector();  }
    }
}
