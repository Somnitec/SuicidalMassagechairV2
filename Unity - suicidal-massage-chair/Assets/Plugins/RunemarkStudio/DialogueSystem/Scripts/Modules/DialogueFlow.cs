namespace Runemark.DialogueSystem
{
    using Runemark.DialogueSystem.UI;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// This class contains the basic parameters for a dialogue flow.
    /// </summary>
    [System.Serializable]
    public class DialogueFlow
    {
        public DialogueBehaviour Behaviour;
        public virtual string EVENT_NAME { get { return ""; } }

        public bool Enabled;
        public Trigger Trigger;
        public bool UseDefaultSkin;
        public string DefaultSkin;

        public Transform Player;

        #region Delegates

        public delegate void TextListener(DialogueFlow dialogue, TextData text, List<TextAnswerData> answers, SettingsData settings);
        public TextListener OnTextChanged;

        public delegate void OnDialogueEvent(DialogueBehaviour owner);
        public OnDialogueEvent OnDialogueFinished;
        public OnDialogueEvent OnDialogueStart;
        public OnDialogueEvent OnDialoguePaused;
        #endregion

        public bool CheckTrigger(Trigger.Modes mode)
        {
            return Enabled && Trigger.Mode == mode;
        }      
    }

    /// <summary>
    /// This class is a dialogue flow extension for conversations.
    /// </summary>
    [System.Serializable]
    public class ConversationFlow : DialogueFlow
    {
        /// <summary>
        /// Name of the Event this flow starts.
        /// </summary>
        public override string EVENT_NAME {  get { return "Conversation"; } }

        /// <summary>
        /// Determines whether or not this flow should reposition the player.
        /// </summary>
        public bool OverridePlayerPosition;
        /// <summary>
        /// If this conversation repositions the player, this transform will store the position.
        /// </summary>
        public Transform PlayerPosition;
        /// <summary>
        /// Tag of the player Game Object.
        /// </summary>
        public string PlayerTag;
        
        /// <summary>
        /// Determines whether or not the dialogue should close the dialogue if the player
        /// is out of the 'use' range.
        /// </summary>
        public bool UseAutoExit = true;

        /// <summary>
        /// Delay the dialogue close by this number of seconds. 
        /// </summary>
        public float ExitDelay = 0f;

        /// <summary>
        /// Distance between the player and the actor, when the dialogue should be closed.
        /// Only used for custom trigger.
        /// </summary>
        public float ExitDistance = 5f;

        /// <summary>
        /// Camera controller.
        /// </summary>
        public CameraController CameraController = new CameraController();

        #region Obselotes
        [System.Obsolete("Use the DialogueFlow.CameraController.Enable", true)]
        public bool UseCustomCameras;
        [System.Obsolete("Use the DialogueFlow.CameraController.DefaultIndex", true)]
        public int DefaultCameraIndex = -1;
        [System.Obsolete("Use the DialogueFlow.CameraController.CameraList", true)]
        public List<Camera> ConversationCameras = new List<Camera>();
        #endregion
    }

    /// <summary>
    /// This class is a dialogue flow extension for ambient dialogues.
    /// </summary>
    [System.Serializable]
    public class AmbientDialogue : DialogueFlow
    {
        /// <summary>
        /// Name of the Event this flow starts.
        /// </summary>
        public override string EVENT_NAME { get { return "Ambient"; } }

        /// <summary>
        /// Offset for the ambient dialogue.
        /// </summary>
        public Vector3 Offset;

        /// <summary>
        /// The ambient dialogue will only show once, when triggered.
        /// </summary>
        public bool Once = false;

        /// <summary>
        /// Time to trigger the Ambient Dialogue.
        /// </summary>
        public float Time = 10;

        /// <summary>
        /// Last time when this ambient dialogue triggered.
        /// </summary>
        public float LastShown = 0;

        public bool TimeCheck(float time)
        {            
            if (Time == 0) return false;
            if (LastShown + Time > time) return false;
            if (Once && Time > 0) return false;
            return true;
        }

        public void OnDisapear(DialogueBehaviour b, float time)
        {
            LastShown = time;
            if (OnDialogueFinished != null)
                OnDialogueFinished(b);
        }
    }
}