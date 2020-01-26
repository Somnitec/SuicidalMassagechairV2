namespace Runemark.DialogueSystem
{
    using UnityEngine;
    using UnityEditor;
    using Runemark.Common;

    [CustomEditor(typeof(DialogueBehaviour), true)]
    public class DialogueBehaviourInspector : RunemarkEditor
    {
        protected virtual string graphLabel { get { return "Dialogue"; } }

        DialogueBehaviour myTarget;
        ReorderableListGUI _cameras;

        GUIStyle _titleStyle;

        SkinNameList _skinNames = new SkinNameList();

        void OnEnable()
        {
            myTarget = (DialogueBehaviour)target;

            _skinNames.CollectSkins(myTarget.Conversation.DefaultSkin, null, 0);
            myTarget.Conversation.CameraController.OnEnable();

            FindProperty("Graph");
            FindProperty("ActorEnabled");
            FindProperty("ActorName");
            FindProperty("ActorPortrait");

            FindProperty("Conversation.Enabled");
            FindProperty("Conversation.Trigger.Mode");
            FindProperty("Conversation.Trigger.PlayerTag");
            FindProperty("Conversation.Trigger.Distance");
            FindProperty("Conversation.UseAutoExit");
            FindProperty("Conversation.ExitDelay");
            FindProperty("Conversation.ExitDistance");
            FindProperty("Conversation.OverridePlayerPosition");
            FindProperty("Conversation.PlayerTag");
            FindProperty("Conversation.PlayerPosition");
            FindProperty("Conversation.UseDefaultSkin");

            FindProperty("AmbientDialogue.Enabled");
            FindProperty("AmbientDialogue.Once");
            FindProperty("AmbientDialogue.Time");
            FindProperty("AmbientDialogue.Trigger.Mode");
            FindProperty("AmbientDialogue.Trigger.PlayerTag");
            FindProperty("AmbientDialogue.Trigger.Distance");
            FindProperty("AmbientDialogue.Offset");

            FindProperty("ExternalEventEnable");

        }

        public override void OnInspectorGUI()
        {
            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle(EditorStyles.boldLabel);
                _titleStyle.alignment = TextAnchor.MiddleCenter;
            }

            DialogueBehaviour myTarget = (DialogueBehaviour)target;
            float width = EditorGUIUtility.currentViewWidth;

            RunemarkGUI.inspectorTitle.Draw("Dialogue Behaviour",
                "This component compiles the dialogue graph in runtime, also it " +
                "commincates with the ui and other systems.");

            serializedObject.Update();

            // DIALOGUE GRAPH
            GUILayout.BeginVertical("box");
            EditorGUILayout.Space();

            DrawPropertyField("Graph", graphLabel);
    
            if (myTarget.Graph == null)
                EditorGUILayout.HelpBox("Step 1: Select a dialogue", MessageType.Error);

            EditorGUILayout.Space();
            GUILayout.EndVertical();

            if (myTarget.Graph != null)
            {
                EditorGUILayout.Space();

                ActorDetails();
                ConversationDetails();
                AmbientDetails();
                ExternalEvents();
            }
            serializedObject.ApplyModifiedProperties();
        }

        void ActorDetails() 
        {
            EditorGUIExtension.FoldoutBox("Actor", ref myTarget.ActorFoldout, (myTarget.ActorEnabled) ? 1 : 0, delegate ()
            {
                EditorGUI.indentLevel--;

                DrawPropertyField("ActorEnabled", "Enable");
                var prop = GetProperty("ActorEnabled");
                if (prop.boolValue)
                {
                    GUIContent label = new GUIContent("Actor Name", "The dialogue system will use this name as Actor name. If it's not set, the name will be the name of the GameObject.");
                    DrawPropertyField("ActorName", label);
                    DrawPropertyField("ActorPortrait", "Portrait");                 
                }
                EditorGUI.indentLevel++;
            });
            EditorGUILayout.Space();
        }
        void ConversationDetails() 
        {
            EditorGUIExtension.FoldoutBox("Conversation", ref myTarget.ConversationFoldout, (myTarget.Conversation.Enabled) ? 1 : 0, delegate () 
            {
                EditorGUI.indentLevel--;

                DrawPropertyField("Conversation.Enabled", "Enable");
                var prop = GetProperty("Conversation.Enabled");
                if (prop.boolValue)
                {
                    EditorGUILayout.Space();

                    DrawPropertyField("Conversation.Trigger.Mode", "Trigger");

                    EditorGUI.indentLevel++;

                    // Player Tag
                    if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.TriggerEnter || myTarget.Conversation.Trigger.Mode == Trigger.Modes.Use)
                        DrawPropertyField("Conversation.Trigger.PlayerTag", "Player Tag");

                    // Distance
                    if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.Use)
                        DrawPropertyField("Conversation.Trigger.Distance", "Distance");

                    // Custom
                    if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.Custom)
                        EditorGUILayout.HelpBox("You can call the StartDialogue() method from script directly.", MessageType.Info);

                    // On Start
                    if (myTarget.Conversation.Trigger.Mode != Trigger.Modes.OnStart)
                    {
                        DrawPropertyField("Conversation.UseAutoExit");
                        var useAutoExit = GetProperty("Conversation.UseAutoExit");
                      
                        if (useAutoExit.boolValue)
                        {
                            DrawPropertyField("Conversation.ExitDelay");
                           
                            string s = "";
                            if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.TriggerEnter)
                                s = " exitst the trigger.";
                            if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.Use)
                                s = " is further from the actor than the use distance.";
                            if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.Custom)
                                s = " is further from the actor than the exit distance.";
                            EditorGUILayout.HelpBox("The dialogue will close if the player" + s, MessageType.Info);
                        }
                    }

                    // Exit Distance
                    if (myTarget.Conversation.Trigger.Mode == Trigger.Modes.Custom)
                        DrawPropertyField("Conversation.ExitDistance", new GUIContent("Exit Distance", "Distance between the player and the actor, when the dialogue should be closed."));
                  
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();

                    // CONVERSATION - PLAYER POSITION

                    DrawPropertyField("Conversation.OverridePlayerPosition", new GUIContent("Override Player Position", "You can specify a position for the player during this conversation"));
                    var overridePlayerPos = GetProperty("Conversation.OverridePlayerPosition");
                    if (overridePlayerPos.boolValue)
                    {
                        EditorGUI.indentLevel++;

                        DrawPropertyField("Conversation.PlayerTag", "Player Tag");
                        DrawPropertyField("Conversation.PlayerPosition", "Position");            
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.Space();

                    // CONVERSATION - CUSTOM CAMERA
                    Undo.RecordObject(myTarget, "Dialogue Behaviour Camera Settings");
                    if(myTarget.Conversation.CameraController.DrawInspectorGUI())
                        PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);

                    EditorGUILayout.Space();

                    // CONVERSATION - SKIN
                    DrawPropertyField("Conversation.UseDefaultSkin", "Use Custom Skin");
                    var customSkin = GetProperty("Conversation.UseDefaultSkin");
                    if (customSkin.boolValue)
                    { 
                        if (_skinNames.DrawGUI())
                        {
                            Undo.RecordObject(myTarget, "Changed Skin Name");
                            myTarget.Conversation.DefaultSkin = SkinDatabase.Instance.Skins[_skinNames.Index].Name;
                            PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);                            
                        }
                    } // +Culling
                }
                EditorGUI.indentLevel++;
            });
            EditorGUILayout.Space();
        }
        void AmbientDetails() 
        {
            // BARKING
            EditorGUIExtension.FoldoutBox("Ambient Dialogue (Barking)", ref myTarget.BarkFoldout, (myTarget.AmbientDialogue.Enabled) ? 1 : 0, delegate () 
            {
                EditorGUI.indentLevel--;

                DrawPropertyField("AmbientDialogue.Enabled", "Enable");
                var prop = GetProperty("AmbientDialogue.Enabled");
                if (prop.boolValue)
                {         
                    EditorGUILayout.Space();

                    DrawPropertyField("AmbientDialogue.Once", new GUIContent("Once", "If this option is turned on, the ambient dialogue only appears once when it's triggered"));
                    if (!myTarget.AmbientDialogue.Once)
                        DrawPropertyField("AmbientDialogue.Time", new GUIContent("Time", "The ambient dialogue will activate after this amount of seconds (if not active currently)"));

                    EditorGUILayout.Space();

                    DrawPropertyField("AmbientDialogue.Trigger.Mode", "Trigger");                 
                    EditorGUI.indentLevel++;

                    // Player Tag
                    if (myTarget.AmbientDialogue.Trigger.Mode == Trigger.Modes.TriggerEnter || myTarget.AmbientDialogue.Trigger.Mode == Trigger.Modes.Use)
                        DrawPropertyField("AmbientDialogue.Trigger.PlayerTag", "Player Tag");

                    // Distance
                    if (myTarget.AmbientDialogue.Trigger.Mode == Trigger.Modes.Use)
                        DrawPropertyField("AmbientDialogue.Trigger.Distance", "Distance");
               
                    if (myTarget.AmbientDialogue.Trigger.Mode == Trigger.Modes.Custom)
                        EditorGUILayout.HelpBox("You can call the StartBark() method from script directly.", MessageType.Info);
                    EditorGUI.indentLevel--;

                    // OFFSET
                    EditorGUILayout.Space();
                    DrawPropertyField("AmbientDialogue.Offset", new GUIContent("Offset", "The offset position for the ambient dialogue ui."));
                    EditorGUILayout.Space();

                    // +Culling
                }
                EditorGUI.indentLevel++;

            });
            EditorGUILayout.Space();
        }
        void ExternalEvents()
        {
            EditorGUIExtension.FoldoutBox("External Events " + ((myTarget.ExternalEventEnable) ? "[" + myTarget.Events.Count + "]" : ""), ref myTarget.ExternalEventFoldout, (myTarget.ExternalEventEnable) ? 1 : 0, delegate () 
            {
                EditorGUI.indentLevel--;

                DrawPropertyField("ExternalEventEnable", "Enable");
                var prop = GetProperty("ExternalEventEnable");
                if (prop.boolValue)
                {
                    EditorGUILayout.Space();

                    for (int cnt = 0; cnt < myTarget.Events.Count; cnt++)
                    {
                        var element = serializedObject.FindProperty("Events").GetArrayElementAtIndex(cnt);

                        EditorGUIExtension.SimpleBox("", 5, "ShurikenModuleBG", delegate () 
                        {                          
                            EditorGUILayout.BeginHorizontal();
                            var eventNameProp = element.FindPropertyRelative("EventName");
                            DrawPropertyField(eventNameProp, new GUIContent("Event Name", "This is the name you want to call from the graph"));             

                            //myTarget.Events[cnt].EventName = EditorGUILayout.TextField(new GUIContent("Event Name", "This is the name you want to call from the graph"), myTarget.Events[cnt].EventName);
                            if (GUILayout.Button("X", GUILayout.Width(20)))
                            {
                                Undo.RecordObject(myTarget, "Removed External Event");
                                myTarget.Events.RemoveAt(cnt);
                                PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);                        
                                return;
                            }
                            EditorGUILayout.EndHorizontal();

                            var eventProperty = element.FindPropertyRelative("uEvent");
                            DrawPropertyField(eventProperty, new GUIContent("Event: " + myTarget.Events[cnt].EventName));
                        });

                        GUILayout.Space(10);
                    }

                    if (GUILayout.Button("Add Event"))
                    {
                        Undo.RecordObject(myTarget, "Added External Event");
                        myTarget.Events.Add(new DialogueBehaviour.ExternalEvent());
                        PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);                        
                        Repaint();
                    }
                }

            });
            EditorGUILayout.Space();
        }

		
	}
}