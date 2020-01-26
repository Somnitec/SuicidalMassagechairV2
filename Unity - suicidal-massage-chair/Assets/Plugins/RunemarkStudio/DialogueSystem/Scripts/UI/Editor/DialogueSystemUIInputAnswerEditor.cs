namespace Runemark.DialogueSystem.UI
{
    using UnityEditor;

    [CustomEditor(typeof(UIElementAnswerInput), true)]
    public class DialogueSystemUIInputAnswerEditor : DialogueSystemUIAnswerEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FindProperty("Label");
            FindProperty("InputUI");
            FindProperty("ElementID");
        }

        protected override void DrawGUI()
        {
            DrawPropertyField("Label");
            DrawPropertyField("InputUI");
            DrawPropertyField("ElementID");
        }
    }
}