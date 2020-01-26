namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using UnityEditor;
    using UnityEngine.UI;

    [CustomEditor(typeof(UIElementTimer), true)]
    public class DialogueSystemUIElementTimerEditor : DialogueSystemUIElementEditor
    {
        private void OnEnable()
        {
            FindProperty("Slider");
            FindProperty("Label");
        }

        protected override void onGUI()
        {
            UIElementTimer myTarget = (UIElementTimer)target;

            EditorGUIExtension.SimpleBox("UI Elements", 5, "", delegate ()
            {
                DrawPropertyField("Slider");
                DrawPropertyField("Label", "Text");
            });
        }      

    }
}
