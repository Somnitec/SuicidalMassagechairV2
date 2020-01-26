namespace Runemark.DialogueSystem.UI
{
    using Runemark.Common;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [CustomEditor(typeof(IconLibrary), true)]
    public class DialogueSystemUIIconLibraryEditor : RunemarkEditor
    {
        IconLibrary _myTarget;
        ReorderableList _iconList;

        private void OnEnable()
        {
            _myTarget = (IconLibrary)target;

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

        public override void OnInspectorGUI()
        {
            var t = _myTarget.GetType().ToString().Split('.');
            string title = t[t.Length - 1].Replace("UI Icon Library", "");
            RunemarkGUI.inspectorTitle.Draw("Dialogue System - " + title, "");

            serializedObject.Update();
            _iconList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
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