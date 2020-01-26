namespace Runemark.DialogueSystem.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIElementAnswer : UIElement
    {
        public virtual int Index { get; set; }

        public KeyCode Key;

        protected IDialogueUIController uiController;
         public string AnswerID;
        Button _button;

        public virtual void Init(IDialogueUIController controller)
        {
            _button = GetComponent<Button>();
            if(_button != null)
                _button.onClick.AddListener(Select);
            uiController = controller;
        }

        private void Update()
        {
            if (Input.GetKeyUp(Key) && _button.onClick != null)
                _button.onClick.Invoke();
        }

        /// <summary>
        /// Select this answer.
        /// </summary>
        public void Select()
        {
            uiController.OnAnswerSelected(AnswerID);
        }
    }
}
