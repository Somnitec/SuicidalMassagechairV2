namespace Runemark.DialogueSystem
{
    using Runemark.VisualEditor;
    using System.Collections.Generic;
    using UnityEngine;

    public class TextData
    {
        public string Name;
        public Sprite Portrait;
        public string Text;

        public AudioClip Audio;
        public float AudioStartTime;
        public float AudioEndTime;
        public float AudioDelay;       
    }
    public class TimerData
    {
        public float Seconds;
        public string OutputName;
    }
    public class TextAnswerData
    {
        public string Text;
        public string OutputName;

        public bool UseCustomUI;
        public string UIElementName;
        public bool IsGlobal;
        public string VariableName;
    }

    public class SettingsData
    {
        public int CameraIndex = -1;
        public string Skin;
    }  

    [System.Obsolete("", true)]
    public class DialogueText
    {
        public readonly string ActorName;
        public readonly string Text;

        public readonly Sprite Portrait;

        public readonly AudioClip Audio;
        public readonly float AudioStartTime;
        public readonly float AudioEndTime;
        public readonly float AudioDelay;

        public readonly int CameraIndex = -1;
        public readonly string Skin;

        public List<AnswerData> answerList = new List<AnswerData>();
        public bool HasTimer;
        public float TimeBack;
        public string TimerOutputName;
        public DialogueText(TextNode node)
        {
            ActorName = node.ActorName;
            Text = node.Text;

            Portrait = node.Portrait;
            Audio = node.Audio;
            AudioStartTime = node.AudioStartTime;
            AudioEndTime = node.AudioEndTime;
            AudioDelay = node.AudioDelay;
            CameraIndex = node.CameraIndex;
            Skin = (node.CustomSkinEnable) ? node.Skin : "";

            answerList = new List<AnswerData>();

        }
    }

    
}