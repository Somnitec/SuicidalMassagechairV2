namespace Runemark.DialogueSystem.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("Runemark Dialogue System/UI/Elements/Text")]
    public class UIElementText : UIElement
    {
        Text _text;

        private void OnEnable()
        {
            _text = Get<Text>();
        }

        public override void Set<T>(T value)
        {
            if (typeof(T) == typeof(string))
                _text.text = value as string;
            else
                base.Set<T>(value);
        }

    }
}