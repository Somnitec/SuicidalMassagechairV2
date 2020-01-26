namespace Runemark.DialogueSystem
{
    using UnityEngine;

    public class ChangeLanguage : MonoBehaviour
    {
        [LanguageList(true)] public int Language;
        public void OnEnable()
        {
            SetLanguage(Language);
        }

        public void SetLanguage(int l)
        {
            LanguageDatabase.Instance.SetCurrentIndex(l);

            // Updating the dialogue behaviour - only necessary if you want it to change the 
            // language from the dialogue in game.
            var dialogueBehaviour = GetComponent<DialogueBehaviour>();
            dialogueBehaviour.SetLocalVariable<int>("Language", l);
        }
    }
}
