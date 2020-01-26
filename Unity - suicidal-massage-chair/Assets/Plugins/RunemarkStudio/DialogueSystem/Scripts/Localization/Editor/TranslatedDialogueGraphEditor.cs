namespace Runemark.DialogueSystem
{
    using Runemark.Common;
    using Runemark.VisualEditor;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [CustomEditorWindow(typeof(TranslatedDialogueGraph), true)]
    public class TranslatedDialogueGraphEditor : RunemarkEditorWindow
    {
        SerializedObject serializedObject;
        SerializedProperty languageProperty;

        ReorderableList textNodes;

        TranslatedDialogueGraph _loadedGraph;
        string _errorMsg = "";

        private void OnEnable()
        {
            titleContent = new GUIContent("Localization");
        }
        protected override void onGUI()
        {            
            string title = "Dialogue Translator";
            string description = "";
            if (_loadedGraph != null)
            {
                title = _loadedGraph.name;
                if (_loadedGraph.Original != null)
                {
                    description = string.Format("This is a {0} translation of the {1} dialogue graph.",
                              LanguageDatabase.Instance[_loadedGraph.Language],
                             _loadedGraph.Original.name);
                }
            }
            RunemarkGUI.inspectorTitle.Draw(title, description);

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                DrawRuntimeUI();
                return;
            }
            if (_loadedGraph == null || serializedObject == null)
            {
                LoadSelectedGraph();
                return;
            }

            serializedObject.Update();
            if (_loadedGraph.Original == null)
                DrawEmptyUI();
            else
                DrawTranslator();
            serializedObject.ApplyModifiedProperties();
        }        
        public override void LoadGraph(object selectedObject)
        {
            _loadedGraph = (TranslatedDialogueGraph)selectedObject;
            serializedObject = new SerializedObject(_loadedGraph);

            if (!_loadedGraph.isInitialized)
            {
                SetDefaultLanguage();
                _loadedGraph.isInitialized = true;
            }           
            LoadTextNodesFromOriginal();

            textNodes = new ReorderableList(serializedObject, serializedObject.FindProperty("translatedTexts"), false, false, false, false);
            textNodes.headerHeight = 5f;
            textNodes.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var text = _loadedGraph.translatedTexts[index];
                string label = text.OriginalText + (text.isOriginalTextChanged ? "*" : "");
                if(label.Length > 40)
                    label = label.Remove(40) + "...";
                    

                Rect statusRect = new Rect(5, rect.y+2.5f, 10, 10);
                GUI.Label(statusRect, "", VisualEditorGUIStyle.ColorBox(TranslationStatus(text)));
                rect.x += 15;
                rect.width -= 15;

                GUI.Label(rect, label);
            };
            textNodes.onSelectCallback = (ReorderableList list) =>
            {
                int i = Mathf.Clamp(list.index, 0, _loadedGraph.translatedTexts.Count);
                _selectedText = _loadedGraph.translatedTexts[i];

                var editor = VisualEditor.GetWindow<VisualEditor>();
                var node = _loadedGraph.Original.Nodes.Find(_selectedText.OriginalID);
                editor.PanToNode(node);


            };
        }        
        void LoadSelectedGraph()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            // Try to get a graph that is selected.
            var selection = Selection.activeObject;
            if (selection != null)
            {
                LoadGraph(selection);
            }
        }
              

        #region Drawing UI
        void DrawErrorField(Rect rect)
        {
            rect.height = 40;
            if (_errorMsg != "" && _errorMsg.Length > 0)
                EditorGUI.HelpBox(rect, _errorMsg, MessageType.Error);
        }
        void DrawRuntimeUI()
        {
            string msg = "You are in play mode! \nYou can't translate text in play mode.";
            Rect rect = new Rect(position.width / 2 - 150, position.height / 2 - 20, 300, 40);
            EditorGUI.HelpBox(rect, msg, MessageType.Warning);
        }

        DialogueGraph _originalCandidate;
        void DrawEmptyUI()
        {          
            string msg = "Assign a Dialogue Graph.";
            Rect rect = new Rect(position.width / 2 - 200, position.height / 2 - 100, 400, 40);
            EditorGUI.HelpBox(rect, msg, MessageType.Info);

            rect.y += rect.height + 10;

            rect.height = 20;
            _originalCandidate = (DialogueGraph)EditorGUI.ObjectField(rect, "Original", _originalCandidate, typeof(DialogueGraph), false);

            rect.y += rect.height + 10;
            if (languageProperty == null) GrabLanguageProperty();
            EditorGUI.PropertyField(rect, languageProperty);
          
            rect.y += rect.height + 10;
            if (GUI.Button(rect, "Setup"))
            {
                if (TryToSetOriginal(_originalCandidate))
                    LoadTextNodesFromOriginal();
            }

            rect.y += rect.height + 10;
            DrawErrorField(rect);           
        }
        void DrawTranslator()
        {
            var searchIcon = RunemarkGUI.Textures.LoadFromEditor("Search Icon");
            Rect rect = new Rect(position.width-40,5,30,30);
            if (GUI.Button(rect, searchIcon, RunemarkGUI.inspectorTitle.titleBackgroundStyle))
            {
                EditorGUIUtility.PingObject(_loadedGraph);
            }

            TextNodeList(new Rect(0, 70, 250, position.height - 80));
            TranslatorUI(new Rect(260, 70, position.width - 270, position.height - 80));
        }

        Vector2 _listScrollPos;
        void TextNodeList(Rect rect)
        {
           int elements = _loadedGraph.translatedTexts.Count;
            _listScrollPos = GUI.BeginScrollView(rect, _listScrollPos, new Rect(0, 0, rect.width-20, elements * 30), false, false);
            textNodes.DoList(new Rect(0, 0, rect.width, elements * 30));
            GUI.EndScrollView();
        }

        Vector2 _detailsScrollPos;
        float _detailContentHeight = 200;

        const float LINE_HEIGHT = 20;
        const float HELPBOX_HEIGHT = 40;
        void TranslatorUI(Rect rect)
        {
            _detailsScrollPos = GUI.BeginScrollView(rect, _detailsScrollPos, new Rect(0, 0, rect.width - 20, rect.height), false, false);
            rect.x = 0; rect.y = 0; rect.width -= 20;
 
            if (_selectedText == null)
            {
                Rect r = new Rect(rect.x + rect.width / 2 - 150, rect.y + rect.height / 2 - 20, 300, HELPBOX_HEIGHT);
                EditorGUI.HelpBox(r, "Please select a node from the left menu!", MessageType.Warning);
            }
            else
            {
                var original = _loadedGraph.Original.Nodes.Find(_selectedText.OriginalID) as TextNode;
                if (original == null)
                {
                    Rect r = new Rect(rect.x + rect.width / 2 - 150, rect.y + rect.height / 2 - 20, 300, HELPBOX_HEIGHT);

                    EditorGUI.HelpBox(r, "This text was removed from the dialogue graph!", MessageType.Error);
                    if (GUI.Button(new Rect(r.x, r.y + r.height + 5, r.width, LINE_HEIGHT), "Delete this Translation"))
                        _loadedGraph.translatedTexts.Remove(_selectedText);
                    return;
                }

                rect.height = _detailContentHeight;                
                GUI.Box(rect,"", VisualEditorGUIStyle.Box());               
                rect.x += 5; rect.width -= 10; 

                rect.height = LINE_HEIGHT;
                GUI.Label(rect, "Text" + (_selectedText.isOriginalTextChanged ? "*" : ""), RunemarkGUI.Styles.H2(TextAnchor.MiddleCenter));

                rect.y += 30;

                // Actor
                if (original.ActorName != "")
                {
                    _selectedText.Actor = EditorGUI.TextField(rect, original.ActorName, _selectedText.Actor);
                    rect.y += rect.height + 5;
                }

                GUIContent text = new GUIContent(original.Text);
                rect.height = GUI.skin.label.CalcHeight(text, rect.width);
             
                GUI.Label(rect, original.Text);
                rect.y += rect.height + 5;

                GUIStyle textArea = GUI.skin.textArea;
                textArea.wordWrap = true;

                rect.height += 50;
                _selectedText.TranslatedText = EditorGUI.TextArea(rect, _selectedText.TranslatedText, textArea);
                rect.y += rect.height + 5;

                rect.height = 20;
                GUI.Label(rect, "Answers", RunemarkGUI.Styles.H2(TextAnchor.MiddleCenter));
                rect.y += rect.height + 5;

                foreach (var answer in _selectedText.Answers)
                {
                    Variable originalAnswer = original.Variables.GetByName(answer.OriginalID);
                    
                    if (originalAnswer == null)
                    {
                        rect.height = HELPBOX_HEIGHT;
                        EditorGUI.HelpBox(rect, "This answer was removed from the text node!", MessageType.Error);
                        rect.y += rect.height + 5;

                        rect.height = LINE_HEIGHT;
                        if (GUI.Button(rect, "Delete this Translation"))
                        {
                            _selectedText.Answers.Remove(answer);
                            return;
                        }
                        rect.y += rect.height + 5;
                    }
                    else
                    {
                        GUIStyle label = RunemarkGUI.Styles.Paragraph(12, TextAnchor.UpperLeft, FontStyle.Normal, true);
                        GUIContent aContent = new GUIContent(originalAnswer.ConvertedValue<string>() + (answer.isOriginalTextChanged ? "*" : ""));
                        rect.height = label.CalcHeight(aContent, rect.width/2);

                        GUI.Label(new Rect(rect.x, rect.y, rect.width/2, rect.height),
                                aContent, label);

                        answer.TranslatedText = EditorGUI.TextField(new Rect(rect.x + rect.width/2, rect.y, rect.width / 2, rect.height), answer.TranslatedText);
                        rect.y += rect.height + 5;
                    }
                }
            }

            _detailContentHeight = rect.y;
            GUI.EndScrollView();
        }

        #endregion

        #region Translation Management
        void SetDefaultLanguage() 
        {
            _loadedGraph.Language = LanguageDatabase.Instance.DefaultIndex;
        }
        bool TryToSetOriginal(DialogueGraph original)
        {
            if (original == null)
            {
                _errorMsg = "Please select a Dialogue Graph!";
                return false;
            }
            if (!original.translator.Add(_loadedGraph))
            {
                _errorMsg = "This dialogue graph already have a translation for " + _loadedGraph.Language + " language.";
                return false;
            }
            _loadedGraph.Original = original;
            return true;
        }
        void GrabLanguageProperty()
        {            
            languageProperty = serializedObject.FindProperty("Language");
        }
              
        void LoadTextNodesFromOriginal()
        {
            if (_loadedGraph.Original == null) return;

            List<TextNode> originalNodes = _loadedGraph.Original.Nodes.FindAll<TextNode>();
            foreach (var original in originalNodes)
            {
                if (_loadedGraph.Contains(original.ID))
                {
                    _loadedGraph[original.ID].OriginalText = original.Text;
                    LoadTextNodeAnswersFromOriginal(original, _loadedGraph[original.ID]);
                }
                else
                {
                    var text = new TranslatedText(original.ID);
                    LoadTextNodeAnswersFromOriginal(original, text);

                    if (text.OriginalText != original.Text)
                    {
                        text.OriginalText = original.Text;
                        text.isOriginalTextChanged = true;
                    }

                    _loadedGraph.translatedTexts.Add(text);
                }
            }
        }
        void LoadTextNodeAnswersFromOriginal(TextNode original, TranslatedText translate)
        {
            foreach (var answer in original.Answers)
            {
                if (answer.Type != AnswerType.Answer) continue;

                var translatedAnswer = translate.GetAnswer(answer.VariableName);
                if (translatedAnswer != null)
                {
                    Variable o = original.Variables.GetByName(answer.VariableName);
                    string originalAnswer = o.ConvertedValue<string>();

                    if (translatedAnswer.OriginalText != originalAnswer)
                    {
                        translatedAnswer.OriginalText = originalAnswer;
                        translatedAnswer.isOriginalTextChanged = true;
                    }
                }
                else
                {
                    translate.Answers.Add(new TranslatedAnswerText(answer.VariableName));
                }
            }
        }
        #endregion



        int _total;
        int _progress;

        
        TranslatedText _selectedText;
        

           

        
       
        void TextDetails(TranslatedText text)
        {/*
            
           
            GUILayout.Space(5);
            EditorGUILayout.BeginVertical(VisualEditorGUIStyle.Box(), GUILayout.Width(600));

            // Answers
           
            EditorGUILayout.EndVertical();*/

        }

        Color TranslationStatus(TranslatedText text)
        {
            bool missingOne = false, missingAll = true;

            if (text.TranslatedText != "") missingAll = false;
            else missingOne = true;
         
            foreach (var a in text.Answers)
            {
                if (a.TranslatedText != "") missingAll = false;
                else missingOne = true;
            }

            if (missingAll) return Color.red;
            else if (missingOne) return new Color(1, 1, 0);
            else return Color.green ;
        }

    }

    [CustomEditor(typeof(TranslatedDialogueGraph))]
    public class TranslatedDialogueGraphInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            if (LanguageDatabase.Instance == null)
            {
                RunemarkGUI.inspectorTitle.Draw("Translated Dialogue Graph", "");
                EditorGUILayout.HelpBox("Language Database is missing.", MessageType.Error);
                return;
            }
            else
            {
                RunemarkGUI.inspectorTitle.Draw("Translated Dialogue Graph", LanguageDatabase.Instance[((TranslatedDialogueGraph)target).Language]);
            }
           
            if (GUILayout.Button("Open Editor"))
            {
                var window = (TranslatedDialogueGraphEditor)EditorWindow.GetWindow(typeof(TranslatedDialogueGraphEditor));
                window.LoadGraph(target);
            }
        }
    }
}