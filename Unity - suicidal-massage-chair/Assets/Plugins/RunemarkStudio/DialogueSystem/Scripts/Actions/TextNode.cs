namespace Runemark.DialogueSystem
{
    using Runemark.VisualEditor;
    using UnityEngine;
    using System.Collections.Generic;

    [System.Serializable]
    [HelpURL("http://runemarkstudio.com/dialogue-system-documentation/#text-node")]
    [Info("Text", "Icons/text", 1)]
    public class TextNode : ExecutableNode
    {
        DialogueBehaviour dialogueBehaviour 
        {
            get 
            {
                if (_dialogueBehaviour == null)
                    _dialogueBehaviour = (DialogueBehaviour)Owner;
                return _dialogueBehaviour;
            }
        }
        DialogueBehaviour _dialogueBehaviour;

        DialogueGraph graph 
        {
            get 
            {
                if (_graph == null) _graph = Root as DialogueGraph;
                return _graph;
            }
        }
        DialogueGraph _graph;

        protected override bool AutoGenerateInTrans  {	get { return true;  } }
		protected override bool AutoGenerateOutTrans {	get { return false; } }
        
		public override string Tooltip { get { return ""; }}

		public string ActorName = "";
		public string Text = "Write your text here...";
		public Sprite Portrait;

		public AudioClip Audio;
	    public float AudioStartTime = 0;
		public float AudioEndTime;
		public float AudioDelay = 0f;

		public bool CustomCameraEnable;
		public int CameraIndex = 0;     

        public bool CustomSkinEnable;
        public string Skin = "";

        public List<AnswerData> Answers = new List<AnswerData>();

		protected override void OnInit()
		{
			
		}
		protected override Variable CalculateOutput(string name)
		{
			return null;
		}

		protected override void OnEnter()
		{           
            TextData text = graph.translator.TranslateText(ID, new TextData() 
            {
                Name = ActorName,
                Portrait = Portrait,
                Text = Text,
                Audio =  Audio,
                AudioStartTime = AudioStartTime,
                AudioEndTime = AudioEndTime,
                AudioDelay = AudioDelay
            });

            text.Text = ReplaceText(text.Text);

            SettingsData settings = new SettingsData()
            {
                CameraIndex = CustomCameraEnable ? CameraIndex : -1,
                Skin = CustomSkinEnable ? Skin : ""
            };
            TimerData timer = null;

            List<TextAnswerData> answers = new List<TextAnswerData>();
            foreach (var answer in Answers)
            {
                Variable show = GetInput(answer.InputName);
                Variable answerText = Variables.GetByName(answer.VariableName);

                if (answer.Type == AnswerType.Time)
                {
                    Variable time = Variables.GetByName(answer.VariableName);

                    timer = new TimerData()
                    { 
                        Seconds = time.ConvertedValue<float>(),
                        OutputName = answer.OutputName
                    };
                    continue;
                }
                else if (show != null && !show.ConvertedValue<bool>())
                    continue;
                
                TextAnswerData answerData = new TextAnswerData();
                FetchCustomAnswerUI(answerText.ConvertedValue<string>(),
                     out answerData.IsGlobal, 
                     out answerData.UIElementName, 
                     out answerData.VariableName, 
                     out answerData.Text);
               
                answerData.Text = ReplaceText(answerData.Text);
                answerData.UseCustomUI = answerData.UIElementName != "";
                answerData.OutputName = answer.OutputName;

                var a = graph.translator.TranslateAnswer(ID, answer.VariableName, answerData);
                answers.Add(a);
            }    
                        
            dialogueBehaviour.ShowText(ID, text, answers, timer, settings);
        }
		protected override void OnUpdate(){}
		protected override void OnExit(){}		
        
		public void SelectAnswer(string outputName)
		{
            _calculatedNextNode = PinCollection.Get(outputName);
			IsFinished = true;
		}
        public void SelectAnswer(int index)
        {
            if (index < 0 || index >= Answers.Count) return;

            string outputName = Answers[index].OutputName;
            SelectAnswer(outputName);
        }

        public override Node Copy(bool runtime = false)
        {
            var copy = (TextNode)base.Copy(runtime);
            copy.ActorName = ActorName;
            copy.Text = Text;
            copy.Portrait = Portrait;

            copy.Answers = new List<AnswerData>();
            foreach (var a in Answers)
            {
                AnswerData copyAnswer = new AnswerData()
                {
                    InputName = a.InputName,
                    OutputName = a.OutputName,
                    Type = a.Type,
                    VariableName = a.VariableName
                };
                copy.Answers.Add(copyAnswer);
            }

            copy.Audio = Audio;
            copy.AudioStartTime = AudioStartTime;
            copy.AudioEndTime = AudioEndTime;
            copy.AudioDelay = AudioDelay;

            copy.CustomCameraEnable = CustomCameraEnable;
            copy.CameraIndex = CameraIndex;

            copy.CustomSkinEnable = CustomSkinEnable;
            copy.Skin = Skin;

            return copy;
    }

        string ReplaceText(string text)
        {
            string result = text;
            result = result.Replace("{actor}", dialogueBehaviour.GetActorName(ActorName));
            if(dialogueBehaviour.Player != null)
                result = result.Replace("{player}", dialogueBehaviour.Player.name);

            foreach (var v in Root.LocalVariables.GetAll())
            {
                result = result.Replace("{local:" + v.Name + "}", v.Value.ToString());
            }
            foreach (var v in DialogueSystem.GetGlobalVariables())
            {
                result = result.Replace("{global:" + v.Name + "}", v.Value.ToString());
            }

            return result;
        }

        void FetchCustomAnswerUI(string text, out bool isGlobal, out string inputType, out string variableName, out string answerText)
        {
            isGlobal = false;
            inputType = "";
            variableName = "";
            answerText = text;

            if (!text.Contains("{local") && !text.Contains("{global")) return;
                    
            string[] temp = text.Split('}');
            answerText = (temp.Length > 1) ? temp[1] : "";

            temp = temp[0].Replace("{", "").Split(':');
            if (temp.Length < 3)
            {
                Debug.LogError(temp[0] + " is not valid. The correct form: {local|global:inputtype:variablename}");
                return;
            }

            isGlobal = temp[0] == "global";
            inputType = temp[1];
            variableName = temp[2];
        }
        
        #region EditorStuffs
        public bool AudioFoldout;
		public bool CameraFoldout;
        public bool AdvancedSettingsFoldout;
        #endregion
    }
    
    public enum AnswerType { Answer, Time }

    [System.Serializable]
    public class AnswerData
    {
        public AnswerType Type;
        public string InputName;
        public string VariableName;
        public string OutputName;
    }



}