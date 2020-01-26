namespace Runemark.DialogueSystem
{
    using System.Collections.Generic;
    using UnityEngine;

    [HelpURL("https://runemarkstudio.com/dialogue-system-documentation/#localization")]
    [CreateAssetMenu(fileName = "new TranslatedDialogue", menuName = "Runemark/Dialogue System/New Dialogue Translation")]
    [System.Serializable] public class TranslatedDialogueGraph : ScriptableObject
    {
        [LanguageList]public int Language;
        public DialogueGraph Original;
        public List<TranslatedText> translatedTexts = new List<TranslatedText>();

        public TranslatedText this[string id]
        {
            get
            {
                foreach (var t in translatedTexts)
                {
                    if (t.OriginalID == id) return t;
                }
                return null;
            }
        }
        public bool Contains(string id)
        {
            return this[id] != null;
        }
  
        #region EDITOR  
        public bool isInitialized = false;
        #endregion
    }

    [System.Serializable] public class TranslatedText : Translation
    {
        public string Actor;
        public List<TranslatedAnswerText> Answers = new List<TranslatedAnswerText>();

        public TranslatedText(string originalID) : base(originalID){}

        public TranslatedAnswerText GetAnswer(string id)
        {   
            foreach (var a in Answers)
            {
                if (a.OriginalID == id) return a;
            }
            return null;
        }
        public bool ContainsAnswer(string id)
        {
            return GetAnswer(id) != null;
        }
    }
    [System.Serializable] public class TranslatedAnswerText : Translation
    {
        public TranslatedAnswerText(string originalVariableName) : base(originalVariableName) { }
    }
    [System.Serializable] public abstract class Translation
    {
        public string OriginalID;
        public bool isOriginalTextChanged;

        public string TranslatedText
        {
            get { return _translatedText; }
            set 
            {
                if (value != _translatedText)
                {
                    _translatedText = value;
                    isOriginalTextChanged = false;
                }
            }
        }
        public string OriginalText
        {
            get { return _originalText; }
            set { _originalText = value;}
        }

        [SerializeField] string _translatedText;
        [SerializeField] string _originalText;                  

        public Translation(string originalID)
        {
            OriginalID = originalID;
        }
    }
    }