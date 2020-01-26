namespace Runemark.DialogueSystem.UI
{
    using Runemark.VisualEditor;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("Runemark Dialogue System/UI/Elements/Input Answer")]
    public class UIElementAnswerInput : UIElementAnswer, ICustomAnswer
    {
        public string ID
        {
            get
            {
                return ElementID;
            }
        }

        public Text Label;
        public InputField InputUI;
        public string ElementID = "input";

        bool isGlobal;
        string variableName;

       
        public override void Init(IDialogueUIController controller)
        {
            var button = GetComponentInChildren<Button>();
            button.onClick.AddListener(Select);
            button.onClick.AddListener(UpdateVariable);
            uiController = controller;           
        }

        void UpdateVariable()
        {
            Variable variable = null;
            if (isGlobal)
                variable = DialogueSystem.GetGlobalVariable(variableName);
            else
            {
                variable = uiController.behavior.GetLocalVariable(variableName);
            }

            if (variable != null)
            {
                switch (InputUI.contentType)
                {
                    case InputField.ContentType.Standard:
                        variable.Value = InputUI.text;
                        break;

                    case InputField.ContentType.IntegerNumber:
                        variable.Value = int.Parse(InputUI.text);
                        break;

                    case InputField.ContentType.DecimalNumber:
                        variable.Value = float.Parse(InputUI.text);
                        break;
                }
            }
            Select();
        }

        public override void Set<T>(T value)
        {
            TextAnswerData data = value as TextAnswerData;
            if (data == null) { Debug.LogError(value + " is not a TextAnswerData!"); return; }
            if (data.UIElementName != ElementID) { return; }

            isGlobal = data.IsGlobal;
            variableName = data.VariableName;
            UnityUIUtility.SetText(Label, data.Text);
        }
    }
}
