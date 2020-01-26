namespace Runemark.DialogueSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using Runemark.VisualEditor;
    using UnityEngine.Events;
    using Runemark.VisualEditor.Actions;

    [HelpURL("http://runemarkstudio.com/dialogue-system-documentation/#dialogue-behaviour")]
    [AddComponentMenu("Runemark Dialogue System/Dialogue Behaviour")]
    public class DialogueBehaviour : VisualEditorBehaviour
    {
        #region Editor Stuffs
        // This part is used to store editor variables.
        public bool ActorFoldout;
        public bool ConversationFoldout;
        public bool BarkFoldout;
        public bool ExternalEventFoldout;
        #endregion

        public bool ActorEnabled;
        public string ActorName;
        public Sprite ActorPortrait;

        public Transform Player { get; private set; }
            
        public ConversationFlow Conversation = new ConversationFlow();
        public AmbientDialogue AmbientDialogue = new AmbientDialogue();

        public TimerData Timer;       
        bool _ambientDialogueActive;

        public bool IsConversationActive { get { return GetActiveNode(Conversation.EVENT_NAME) != null; } }
        public bool IsAmbientDialogueActive { get { return _ambientDialogueActive && !IsConversationActive; } }

        void Awake()
        {
            // Initalize and reset cameras.
        //    _mainCamera = Camera.main;
         //   ResetCamera(Conversation);

            // If custom actor is not enabled, use the game object name if needed.
            if (!ActorEnabled) ActorName = gameObject.name;

            Conversation.Behaviour = this;
            AmbientDialogue.Behaviour = this;

            Conversation.CameraController.Init();
        }

        protected override void OnUpdate()
        {
            if (Timer != null)
            {
                Timer.Seconds -= Time.deltaTime;
                if (Timer.Seconds <= 0f)
                    SelectAnswer(Timer.OutputName);
            }

            // Ambient Dialogue automatically triggers after certain seconds.
            if (IsAmbientDialogueActive && !IsConversationActive && AmbientDialogue.TimeCheck(Time.time))
                CallEvent(AmbientDialogue.EVENT_NAME);

            // Auto close conversation
            if (IsConversationActive && Conversation.UseAutoExit && Conversation.Player != null)
            {
                float distance = Vector3.Distance(transform.position, Conversation.Player.position);
                if(Conversation.Trigger.Mode == Trigger.Modes.Custom)
                {
                    Debug.Log("Distance: " + distance);

                    if (distance > Conversation.ExitDistance)
                        StopDialogue(Conversation);
                }
                else if (Conversation.Trigger.Mode == Trigger.Modes.Use)
                {
                    if (distance > Conversation.Trigger.Distance)
                        StopDialogue(Conversation);
                }
            }
        }

        #region Triggering Dialogues
        void Start()
        {
            // Start the dialogues if it's trigger set to OnStart.
            StartDialogue(Conversation, Trigger.Modes.OnStart);
            StartDialogue(AmbientDialogue, Trigger.Modes.OnStart);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            StartDialogue(Conversation, Trigger.Modes.TriggerEnter, collision.transform);
            StartDialogue(AmbientDialogue, Trigger.Modes.TriggerEnter, collision.transform);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (IsConversationActive && Conversation.UseAutoExit &&
               Conversation.Trigger.Mode == Trigger.Modes.TriggerEnter &&
               Conversation.Player == collision.transform)
            {
                StopDialogue(Conversation);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            StartDialogue(Conversation, Trigger.Modes.TriggerEnter, other.transform);
            StartDialogue(AmbientDialogue, Trigger.Modes.TriggerEnter, other.transform);
        }
        void OnTriggerExit(Collider other)
        {        
            if (IsConversationActive && Conversation.UseAutoExit && 
                Conversation.Trigger.Mode == Trigger.Modes.TriggerEnter &&
                Conversation.Player == other.transform)
            {
                StopDialogue(Conversation);
            }
        }
        public void Use(Transform other)
        {
            if (Conversation.Trigger.Use(other, transform))
                StartDialogue(Conversation, Trigger.Modes.Use, other.transform);
            if (AmbientDialogue.Trigger.Use(other, transform))
                StartDialogue(AmbientDialogue, Trigger.Modes.Use, other.transform);
        }

        /// <summary>
        /// Starts the dialogue. Only works if the trigger on the behaviour is set to Custom.
        /// </summary>
        public void StartDialogue(Transform player)
        {            
            if (Conversation.Trigger.Mode == Trigger.Modes.Custom)
                StartDialogue(Conversation, Trigger.Modes.Custom, player);
            if (AmbientDialogue.Trigger.Mode == Trigger.Modes.Custom)
                StartDialogue(AmbientDialogue, Trigger.Modes.Custom, player);
        }
        #endregion
        #region Camera Controll
     /*   Camera _mainCamera;
        Camera _activeCamera;

        /// <summary>
        /// Sets the camera that the Conversation Flow should use.
        /// </summary>
        /// <param name="flow"></param>
        void SetCamera(ConversationFlow flow, int index)
        {
            if (flow.CameraController.Enable)
            {
                if (index == -1 || index > flow.CameraController.CameraList.Count)
                    index = flow.CameraController.DefaultIndex;

                _activeCamera.enabled = false;

                Debug.Log("Camera index: " + index);

                if (index < 0)
                    _activeCamera = _mainCamera;
                else
                {
                    index = Mathf.Min(index, flow.CameraController.CameraList.Count);
                    _activeCamera = flow.CameraController.CameraList[index];
                }
                _activeCamera.enabled = true;
            }
        }

        /// <summary>
        /// Reset cameras to default: all turn off, except main camera.
        /// </summary>
        void ResetCamera(ConversationFlow flow)
        {
            if (_mainCamera != null)
            {
                _mainCamera.enabled = true;
                _activeCamera = _mainCamera;
            }

            if (flow.UseCustomCameras)
            {
                for (int cnt = 0; cnt < flow.ConversationCameras.Count; cnt++)
                    flow.ConversationCameras[cnt].enabled = false;
            }
        }*/
        #endregion
    
        /// <summary>
        /// Starts the dialogue with the given trigger mode, if it's allowed.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="mode"></param>
        public void StartDialogue(DialogueFlow dialogue, Trigger.Modes mode, Transform player = null)
        {
            if (dialogue.CheckTrigger(mode))
            {
                Player = player;
                dialogue.Player = player;
                CallEvent(dialogue.EVENT_NAME);

                if (dialogue == Conversation && IsAmbientDialogueActive)
                    StopDialogue(AmbientDialogue);
            }
        }
        /// <summary>
        /// Stops the given dialogue flow.
        /// </summary>
        /// <param name="dialogue"></param>
        public void StopDialogue(DialogueFlow dialogue)
        {
            if (dialogue.EVENT_NAME == AmbientDialogue.EVENT_NAME)
                OnAmbientDialogueEnd();
            else
                OnEventFinished(dialogue.EVENT_NAME);
        }


        #region Event Handling

        public bool ExternalEventEnable;
        [System.Serializable]
        public class ExternalEvent
        {
            public string EventName;
            public UnityEvent uEvent;
        }
        public List<ExternalEvent> Events = new List<ExternalEvent>();

        public override void CallEvent(string eventName)
        {
            var e = Events.Find(x => x.EventName == eventName);

            if (e != null)
            {
                e.uEvent.Invoke();
            }
            else
                base.CallEvent(eventName);
        }
        #endregion

        public string GetActorName(string defaultName)
        {
            if (defaultName == "" && ActorEnabled)
            {
                defaultName = ActorName != "" ? ActorName : gameObject.name;
            }
            return defaultName;
        }

        public void ShowText(string ID, TextData text, List<TextAnswerData> answers, TimerData timer, SettingsData settings)
        {
            // SET TIMER   
            Timer = timer;

            text.Name = GetActorName(text.Name);

            if (text.Portrait == null && ActorEnabled)
            {
                text.Portrait = ActorPortrait;
            }
  
            string flow = EventNameofNode(ID);
            if (Conversation.EVENT_NAME == flow)
            {
                if (settings.CameraIndex == -1 && Conversation.CameraController.Enable)
                    settings.CameraIndex = Conversation.CameraController.DefaultIndex;
                   
                if(settings.CameraIndex > -1)
                    Conversation.CameraController.Set(settings.CameraIndex);
                else
                    Conversation.CameraController.Reset();

                if (Conversation.OnTextChanged != null)
                    Conversation.OnTextChanged(Conversation, text, answers, settings);
            }
            else if (AmbientDialogue.EVENT_NAME == flow)
            {
                if (AmbientDialogue.OnTextChanged != null)
                    AmbientDialogue.OnTextChanged(AmbientDialogue, text, answers, settings);
            }
        }
       
        public void PauseText(string eventName)
        {
            if (Conversation.EVENT_NAME == eventName)
            {
               if (Conversation.OnDialoguePaused != null)
                    Conversation.OnDialoguePaused(this);
            }

            else if (AmbientDialogue.EVENT_NAME == eventName)
            {
                if (AmbientDialogue.OnDialoguePaused != null)
                    AmbientDialogue.OnDialoguePaused(this);
            }
        }
        public void SelectAnswer(string outputName)
        {
            var n = GetActiveNode(Conversation.EVENT_NAME);
            if (n == null)
                n = GetActiveNode(AmbientDialogue.EVENT_NAME);

            if (n != null && n.GetType() == typeof(TextNode))
                ((TextNode)n).SelectAnswer(outputName);

            Timer = null;
        }

        public void SelectAnswer(int index)
        {
            var n = GetActiveNode(Conversation.EVENT_NAME);
            if (n == null)
                n = GetActiveNode(AmbientDialogue.EVENT_NAME);

            if (n != null && n.GetType() == typeof(TextNode))
                ((TextNode)n).SelectAnswer(index-1);

            Timer = null;
        }

        protected override void OnEventStarted(string eventName)
        {
            if (Conversation.EVENT_NAME == eventName)
            {
                if (Conversation.OnDialogueStart != null)
                    Conversation.OnDialogueStart(this);

                // Handle reposition of the player.
                if (Conversation.OverridePlayerPosition && Conversation.PlayerPosition != null)
                {
                    var p = GameObject.FindGameObjectWithTag(Conversation.PlayerTag);
                    p.transform.position = Conversation.PlayerPosition.position;
                    p.transform.LookAt(new Vector3(Conversation.PlayerPosition.position.x, p.transform.position.y, Conversation.PlayerPosition.position.z));
                }
            }
            else if (AmbientDialogue.EVENT_NAME == eventName && AmbientDialogue.OnDialogueStart != null)
            {
                _ambientDialogueActive = true;
                AmbientDialogue.OnDialogueStart(this);
            }

            base.OnEventStarted(eventName);
        }
        protected override void OnEventFinished(string eventName)
        {
              if (eventName == Conversation.EVENT_NAME)
            {
                base.OnEventFinished(eventName);
                Conversation.CameraController.Reset();
                if (Conversation.OnDialogueFinished != null)
                    Conversation.OnDialogueFinished(this);
            }
            else if (eventName == AmbientDialogue.EVENT_NAME)
            {
                base.OnEventFinished(AmbientDialogue.EVENT_NAME);
                AmbientDialogue.OnDisapear(this, Time.time);                
            }
        }

        protected override void OnNodeActivated(string eventName, Node node)
        {
            if (node.GetType() == typeof(Wait))
            {
                PauseText(eventName);
            }            
        }

        void OnAmbientDialogueEnd()
        {
            base.OnEventFinished(AmbientDialogue.EVENT_NAME);
            if (AmbientDialogue.OnDialogueFinished != null)
                AmbientDialogue.OnDialogueFinished(this);
            _ambientDialogueActive = false;
        }     
    }
}