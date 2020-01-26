namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;
    using UnityEngine.UI;

    [CustomEditor(typeof(UIElementAnswer), true)]
    public class DialogueSystemUIAnswerEditor : DialogueSystemUIElementEditor
    {
        protected virtual void OnEnable()
        {
            FindProperty("Key");
        }

        protected override void onGUI()
        {
            EditorGUIExtension.SimpleBox("UI Elements", 5, "", delegate ()
            {
                DrawGUI();
            });

            EditorGUIExtension.SimpleBox("Key Binding", 5, "", delegate ()
            {
                DrawPropertyField("Key");            
            });
        }

        protected virtual void DrawGUI() { }
    }

    [CustomEditor(typeof(UIElementAnswerText), true)]
    public class DialogueSystemUITextAnswerEditor : DialogueSystemUIAnswerEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FindProperty("IndexUI");
            FindProperty("TextUI");
        }

        protected override void DrawGUI()
        {
            DrawPropertyField("IndexUI");
            DrawPropertyField("TextUI");
        }
    }

    [CustomEditor(typeof(UIElementAnswerIcon), true)]
    public class DialogueSystemUIIconAnswerEditor : DialogueSystemUIAnswerEditor
    {
        UIElementAnswerIcon _myTarget;
        ReorderableList _iconList;

        protected override void OnEnable()
        {
            _myTarget = (UIElementAnswerIcon)target;

            base.OnEnable();
            FindProperty("Image");
            FindProperty("Icons");

            var iconsProp = GetProperty("Icons");

            _iconList = new ReorderableList(serializedObject, iconsProp, true, true, true, true);
            _iconList.elementHeight = 50;
            _iconList.drawHeaderCallback = DrawIconListHeader;
            _iconList.drawElementCallback = DrawIconListElement;
            _iconList.onAddCallback = (ReorderableList list) =>
            {
                _myTarget.Icons.Add(new IconData());
                PrefabUtility.RecordPrefabInstancePropertyModifications(_myTarget);
            };
            _iconList.onRemoveCallback = (ReorderableList list) => 
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
                PrefabUtility.RecordPrefabInstancePropertyModifications(_myTarget);
            };
        }
        protected override void DrawGUI()
        {
            DrawPropertyField("Image");

            var lib = _myTarget.GetComponentInParent<IconLibrary>();
            if (lib == null)
                _iconList.DoLayoutList();
            else
            {
                _myTarget.Icons.Clear();
                EditorGUILayout.HelpBox("This element uses Icons from an IconLibrary parent component", MessageType.Info);
                if (GUILayout.Button("Select Icon Library"))
                {
                    Selection.activeGameObject = lib.gameObject;
                }
            }
        }

        void DrawIconListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Icons");
        }
        void DrawIconListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = _iconList.serializedProperty.GetArrayElementAtIndex(index);
            var elementName = element.FindPropertyRelative("Name");

            Rect r = new Rect(rect.x + 5, rect.y + 5, rect.height - 10, rect.height - 10);
            Undo.RecordObject(_myTarget, "Assign Icon");
            _myTarget.Icons[index].Icon = (Sprite)EditorGUI.ObjectField(r, _myTarget.Icons[index].Icon, typeof(Sprite), true);
            PrefabUtility.RecordPrefabInstancePropertyModifications(_myTarget);

            r = new Rect(rect.x + rect.height + 10, rect.y + (rect.height - 20) / 2, rect.width - rect.height - 15, 20);
            EditorGUI.PropertyField(r, elementName, GUIContent.none);          
        }
    }
}