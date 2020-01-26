namespace Runemark.DialogueSystem
{
    using System.Collections.Generic;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditorInternal;
    using Runemark.Common;
#endif

    [System.Serializable]
    [HelpURL("https://runemarkstudio.com/dialogue-system-documentation/#localization")]
    [CreateAssetMenu(fileName = "LanguageDatabase", menuName = "Runemark/Dialogue System/Language Database")]
    public class LanguageDatabase : ScriptableObject
    {       
        #region Singleton
        public static LanguageDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<LanguageDatabase>("LanguageDatabase");
                }
               
                return _instance;
            }
        }
        static LanguageDatabase _instance;
        #endregion

        public int Count { get { return languages.Count; } }
        public string this[int index]
        {
            get
            {
                if (languages == null || languages.Count == 0)
                    return "";

                if (index >= 0 && index < languages.Count)
                    return languages[index];
                
                return languages[0];
            }
        }
        public string Current { get { return this[CurrentIndex]; }}
        public string Default { get { return this[DefaultIndex]; }}

        [LanguageList]public int CurrentIndex;
        [LanguageList]public int DefaultIndex = 0;

        [SerializeField] List<string> languages = new List<string>();

        public void SetDefaultIndex(int index)
        {
            if (index >= 0 && index < languages.Count)
                DefaultIndex = index;
            else
                DefaultIndex = 0;
        }
        public void SetCurrentIndex(int index)
        {
            if (index < 0 || index >= languages.Count) index = DefaultIndex;
            CurrentIndex = index;
        }    
   
    }

    public class LanguageListAttribute : PropertyAttribute
    {
        public bool SelectButton;
        public LanguageListAttribute(bool selectButton = false)
        {
            SelectButton = selectButton;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LanguageDatabase))]
    public class LanguageDatabaseEditor : Editor    
    {
        LanguageDatabase myTarget;
        SerializedProperty defaultProperty;
        ReorderableList languages;

        void OnEnable()
        {
            myTarget = (LanguageDatabase)target;
            
            defaultProperty = serializedObject.FindProperty("DefaultIndex");

            languages = new ReorderableList(serializedObject, serializedObject.FindProperty("languages"), false, true, true, true);
            languages.drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "Languages");
            };
            languages.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = languages.serializedProperty.GetArrayElementAtIndex(index);

                bool isDefault = EditorGUI.Toggle(new Rect(rect.x, rect.y, rect.height, rect.height), defaultProperty.intValue == index);
                if (isDefault != (defaultProperty.intValue == index))
                {
                    myTarget.SetDefaultIndex(isDefault ? index : 0);
                    Repaint();
                    return;
                }
                rect.x += rect.height + 5;
                rect.width -= rect.height + 5;

                EditorGUI.PropertyField(rect, element, new GUIContent());
            };
        }

        public override void OnInspectorGUI()
        {
            RunemarkGUI.inspectorTitle.Draw("Dialogue System Languages", "");

            EditorGUILayout.LabelField("Default Language", myTarget.Default);

            serializedObject.Update();      
            languages.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
  
    [CustomPropertyDrawer(typeof(LanguageListAttribute))]
    public class LanguageListDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
             // First get the attribute since it contains the range for the slider
            LanguageListAttribute attr = attribute as LanguageListAttribute;
            if (attr.SelectButton)
            {
                Rect rect = new Rect(position.x + position.width - 25, position.y+3, 25, position.height);
                if(GUI.Button(rect, "", (GUIStyle)"PaneOptions"))
                {
                    Selection.activeObject = LanguageDatabase.Instance;
                    EditorGUIUtility.PingObject(LanguageDatabase.Instance);
                }
                position.width -= 30;
            }

            int languageCount = LanguageDatabase.Instance.Count;
            GUIContent[] labels = new GUIContent[languageCount];
            int[] options = new int[languageCount];
            for (int i = 0; i < languageCount; i++)
            {          
                labels[i] = new GUIContent(LanguageDatabase.Instance[i]);
                options[i] = i;
            }

            EditorGUI.IntPopup(position, property, labels, options);        
        }
    }

#endif
}