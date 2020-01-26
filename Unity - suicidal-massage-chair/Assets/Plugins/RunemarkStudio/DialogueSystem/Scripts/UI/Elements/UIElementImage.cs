namespace Runemark.DialogueSystem.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("Runemark Dialogue System/UI/Elements/Image")]
    public class UIElementImage : UIElement
    {
        Image _image;

        private void OnEnable()
        {
            _image = Get<Image>();
        }

        public override void Set<T>(T value)
        {
            if (typeof(T) == typeof(Sprite))
                _image.sprite = value as Sprite;
            else
                base.Set<T>(value);
        }

    }
}