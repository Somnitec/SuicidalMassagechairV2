namespace Runemark.DialogueSystem.UI
{
    using System.Collections;
    using UnityEngine;

    [AddComponentMenu("Runemark Dialogue System/UI/Elements/TextTyping")]
    public class UIElementTextTyping : UIElementText
    {
        public float TypingSpeed = .5f;
        string _text;

        public override void Set<T>(T value)
        {
            if (typeof(T) == typeof(string))
            {
                StartCoroutine(AnimateText(value as string));
            }
            else
                base.Set<T>(value);           
        }

        IEnumerator AnimateText(string strComplete)
        {
            int i = 0;
            base.Set("");

            while (i < strComplete.Length)
            {
                _text += strComplete[i++];
                base.Set(_text);
                yield return new WaitForSeconds(TypingSpeed);
            }
        }
    }
}