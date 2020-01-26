namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(DialogueSystemUISkin), true)]
    public class DialogueSystemUISkinEditor : RunemarkEditor
    {
        DialogueSystemUISkin myTarget;

        bool _testFoldout;
        MessageType _testMessageType = MessageType.Info;
        string _testMessage = "Click on the test button to check if the skin is setup correctly.";

        void OnEnable()
        {
            myTarget = (DialogueSystemUISkin)target;
            UpdateSkinNames((int)myTarget.UIType);

            FindProperty("Name");
            FindProperty("ActorText");
            FindProperty("ActorName");
            FindProperty("ActorPortrait");
            FindProperty("UseActorName");
            FindProperty("UseActorPortrait");
            FindProperty("Timer");
            FindProperty("UseTimer");
            FindProperty("UIType");
            FindProperty("DynamicAnswerNumber");
            FindProperty("answerKeyBinding");
            FindProperty("FirstOnlyKey");
        }
        public override void OnInspectorGUI()
        {
            RunemarkGUI.inspectorTitle.Draw("Dialogue System UI Skin", "");

            serializedObject.Update();

            SkinSettings();
            ActorDetails();
            AnswerSettings();

            EditorGUIExtension.FoldoutBox("Test", ref _testFoldout, -1, delegate ()
            {
                EditorGUILayout.HelpBox(_testMessage, _testMessageType);
                if (GUILayout.Button("Test Skin"))
                    _testMessageType = (myTarget.Test(out _testMessage)) ? MessageType.Error : MessageType.Info;
            });

            serializedObject.ApplyModifiedProperties();
        }

        void SkinSettings()
        {
            // NAME            
            if (_skinNames.DrawGUI())
            {
                Undo.RecordObject(myTarget, "Changed Skin Name");
                myTarget.Name = SkinDatabase.Instance.Skins[_skinNames.Index].Name;
                PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);
            }

            DrawPropertyField("UIType");

            var uiTypeProp = GetProperty("UIType");
            if((int)myTarget.UIType != uiTypeProp.enumValueIndex)
                UpdateSkinNames(uiTypeProp.enumValueIndex);              
        }
        void ActorDetails()
        {
            EditorGUIExtension.SimpleBox("UI Elements", 5, "", delegate ()
            {
                DrawPropertyField("ActorText", "Text");
                GUILayout.Space(10);

                DrawPropertyFieldWithToggle("ActorName", "UseActorName");
                DrawPropertyFieldWithToggle("ActorPortrait", "UseActorPortrait");

                if (myTarget.UIType != DialogueUIType.AmbientDialogue)
                {
                    DrawPropertyFieldWithToggle("Timer", "UseTimer");
                }
            });
        }
        void AnswerSettings()
        {
            if (myTarget.UIType == DialogueUIType.AmbientDialogue) return;
        
            EditorGUIExtension.SimpleBox("Answers", 5, "", delegate ()
            {
                DrawPropertyField("DynamicAnswerNumber", "Dynamicaly Expand");        
                EditorGUILayout.HelpBox("If Dynamicaly Expand is turned on, " +
                    "in runtime the system will generate additional answer buttons" +
                    "if there is more answer in the text node than button in the ui skin.", MessageType.Info);

                DrawPropertyField("answerKeyBinding", "Bind Key to Answer");          
                switch (myTarget.answerKeyBinding)
                {
                    case DialogueSystemUISkin.AnswerKeyBinding.Alphanumeric1To9:
                        EditorGUILayout.HelpBox("In runtime the system will bind alphanumeric keys to the answers based on their order. Only handles up 9 answers.", MessageType.Info);
                        break;

                    case DialogueSystemUISkin.AnswerKeyBinding.FirstOnly:
                        EditorGUILayout.HelpBox("In runtime the system will bind the below key to the first answer. Great for next or skip type of answers.", MessageType.Info);
                        DrawPropertyField("FirstOnlyKey", "Key");                       
                        break;
                }
            });
          
        }





        SkinNameList _skinNames = new SkinNameList();          
        void UpdateSkinNames(int mode)
        {
            _skinNames.CollectSkins(myTarget.Name, null, mode);
        }



      
    }
}
