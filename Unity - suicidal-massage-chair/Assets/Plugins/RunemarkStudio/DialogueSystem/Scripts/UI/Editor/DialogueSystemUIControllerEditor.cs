namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public class UIControllerEditor<T> : Editor where T : MonoBehaviour, IDialogueUIController
    {
        protected virtual string title { get { return ""; } }
        protected virtual string description { get { return ""; } }
        protected virtual DialogueUIType uiType { get { return DialogueUIType.AmbientDialogue; } }

        SkinNameList _skinNames = new SkinNameList();
        List<string> _childSkins = new List<string>();

        void OnEnable()
        {
            T myTarget = target as T;

            _childSkins.Clear();
            foreach (var c in myTarget.GetComponentsInChildren<DialogueSystemUISkin>())
            {
                if (c.UIType == uiType)
                    _childSkins.Add(c.Name);
            }
            _skinNames.CollectSkins(myTarget.DefaultSkin, _childSkins);
        }

        public override void OnInspectorGUI()
        {
            RunemarkGUI.inspectorTitle.Draw(title, description);
                        
            T myTarget = (T)target;

            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("SkinName", myTarget.DefaultSkin);
            }
            else if (_childSkins.Count == 0)
                EditorGUILayout.HelpBox("You need to add a skin to this element as a child!", MessageType.Error);
            else
            {
                if (_skinNames.DrawGUI(false))
                {
                    Undo.RecordObject(myTarget, "Changed Skin Name");
                    myTarget.DefaultSkin = SkinDatabase.Instance.Skins[_skinNames.Index].Name;
                    PrefabUtility.RecordPrefabInstancePropertyModifications(myTarget);
                }                                     

                if (!_childSkins.Contains(myTarget.DefaultSkin))
                    EditorGUILayout.HelpBox("This skin doesn't exists in this Dialogue UI hierarchy!", MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Default Skin can be override by the Dialogue Behaviour component or by the active Text Node setting.\n" +
                       "You can select the default skin from the skins that are childs of this object.", MessageType.None);
            }
            EditorGUILayout.Space();
        }              
    }


    [CustomEditor(typeof(ConversationController), true)]
    public class ConversationControllerEditor : UIControllerEditor<ConversationController>
    {
        protected override string title
        {
            get
            {
                return "Conversation Controller";
            }
        }
        protected override string description
        {
            get
            {
                return base.description;
            }
        }
        protected override DialogueUIType uiType
        {
            get
            {
                return DialogueUIType.Conversation;
            }
        }
    }

    [CustomEditor(typeof(AmbientDialogueController), true)]
    public class AmbientDialogueControllerEditor : UIControllerEditor<AmbientDialogueController>
    {
        protected override string title
        {
            get
            {
                return "Ambient Dialogue Controller";
            }
        }
        protected override string description
        {
            get
            {
                return base.description;
            }
        }
        protected override DialogueUIType uiType
        {
            get
            {
                return DialogueUIType.AmbientDialogue;
            }
        }
    }

}
