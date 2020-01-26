namespace Runemark.DialogueSystem
{
    using UnityEngine;
    using System.Collections.Generic;

    [System.Serializable]
    public class Translator
    {
        public TranslatedDialogueGraph this[int langIndex]
        {
            get
            {
                foreach (var t in translations)
                {
                    if (t.Language == langIndex)
                        return t;
                }
                return null;
            }
        }
        public TranslatedDialogueGraph current
        {
            get 
            {
                return this[LanguageDatabase.Instance.CurrentIndex];
            }
        }

        [SerializeField] List<TranslatedDialogueGraph> translations = new List<TranslatedDialogueGraph>();

        public bool Add(TranslatedDialogueGraph graph)
        {
            translations.RemoveAll(x => x == null);

            if (translations.Contains(graph)) return false;

            foreach (var t in translations)
            {
                if (t != null && t.Language == graph.Language)
                    return false;
            }

            translations.Add(graph);
            return true;
        }

        public TextData TranslateText(string textId, TextData text)
        {
            if (current == null) return text;

            var tText = current[textId];
            if (tText == null) return text;

           if (tText.Actor != "")
                text.Name = tText.Actor;
            if (tText.TranslatedText != "")
                text.Text = tText.TranslatedText;

            return text;
        }
        public TextAnswerData TranslateAnswer(string textId, string answerID, TextAnswerData answer)
        {
            if (current == null) return answer;

            var tText = current[textId];
            if (tText == null) return answer;

            var tAnswer = tText.GetAnswer(answerID);
            if (tAnswer == null) return answer;

            answer.Text = tAnswer.TranslatedText;

            return answer;
        }
    }
}