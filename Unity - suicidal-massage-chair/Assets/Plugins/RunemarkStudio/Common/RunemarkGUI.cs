namespace Runemark.Common
{
    using System.Collections.Generic;
    using UnityEngine;

    #if UNITY_EDITOR
    using UnityEditor;

    public class RunemarkGUI
    {
        public class TextureData
        {
            Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

            public Texture2D this[string key]
            {
                get
                {
                    Texture2D texture = null;                    
                    if (_textures.ContainsKey(key)) texture = _textures[key];
                    return texture;
                }
                set
                {
                    if (_textures.ContainsKey(key)) _textures[key] = value;
                    else _textures.Add(key, value);
                }
            }
            public Texture2D LoadFromResources(string name, string path = "")
            {
                Texture2D texture = this[name];
                if (texture != null) return texture;

                if (path == "") path = name;
                texture = Resources.Load<Texture2D>(path);
                if (texture != null) this[name] = texture;

                return texture;
            }

#if UNITY_EDITOR
            public Texture2D LoadFromEditor(string name)
            {
                Texture2D texture = this[name];
                if (texture == null)
                {
                    texture = EditorGUIUtility.FindTexture(name);
                    if (texture != null) this[name] = texture;
                }
                return texture;
            }
#endif
        }
        public class GUIStyleData
        {
            Dictionary<string, GUIStyle> _styles = new Dictionary<string, GUIStyle>();
            public GUIStyle this[string key]
            {
                get
                {
                    if (_styles.ContainsKey(key)) return _styles[key];
                    return null;
                }
                set
                {
                    if (_styles.ContainsKey(key)) _styles[key] = value;
                    else _styles.Add(key, value);
                }
            }

            public GUIStyle H1(TextAnchor align)
            {
                return Paragraph(16, align, FontStyle.Bold);
            }
            public GUIStyle H2(TextAnchor align)
            {
                return Paragraph(12, align, FontStyle.Bold);
            }

            public GUIStyle Paragraph(int size, TextAnchor align, FontStyle fontStyle, bool wordWrap = false)
            {
                return Paragraph(size, align, fontStyle, Color.black, wordWrap);
            }
            public GUIStyle Paragraph(int size, TextAnchor align, FontStyle fontStyle, Color color, bool wordWrap = false)
            {
                string key = "Paragraph" + size + align + fontStyle + wordWrap+color;
                GUIStyle style = this[key];
                if (style == null)
                {
                    style = new GUIStyle(GUI.skin.label);
                    style.alignment = align;
                    style.fontSize = size;
                    style.fontStyle = fontStyle;
                    style.wordWrap = wordWrap;
                    style.normal.textColor = color;
                    this[key] = style;
                }
                return style;
            }

        }

        public static TextureData Textures = new TextureData();
        public static GUIStyleData Styles = new GUIStyleData();
        public static InspectorTitle inspectorTitle = new InspectorTitle();

        public class InspectorTitle
        {
            Color titleBackground { get { return new Color(0.25f, 0.26f, 0.31f, 1.00f); } }
            Color titleTextColor { get { return new Color(0.85f, 0.85f, 0.85f, 1.00f); } }
            
            GUIStyle iconStyle
            {
                get
                {
                    string key = "InspectorIconStyle";
                    var style = Styles[key];

                    if (style == null)
                    {
                        var icon = Textures["InspectorTitleIcon"];
                        if (icon == null) return titleBackgroundStyle;
            
                        style = new GUIStyle(GUI.skin.box);
                        style.normal.background = icon;
                        style.padding = new RectOffset(0, 0, 0, 0);
                        style.margin = new RectOffset(0, 0, 0, 0);                      

                        Styles[key] = style;
                    }
                    return style;
                }
            }
            public GUIStyle titleBackgroundStyle
            {
                get
                {
                    string key = "TitleBackgroundStyle";
                    var style = Styles[key];

                    if (style == null)
                    {
                        style = new GUIStyle(GUI.skin.box);
                        style.padding = new RectOffset(3, 3, 3, 3);
                        Styles[key] = style;
                    }

                    if (style.normal.background == null || style.normal.background.name == "OL box")
                    {
                        var text = new RectangleTexture();
                        text.Resolution = 2;
                        text.FillColor = titleBackground;
                        text.BorderColor = titleBackground;
                        Texture2D texture = text.Generate();

                        style.normal.background = texture;
                    }                        
                    
                    return style;
                }
            }
            GUIStyle inspectorTitleStyle
            {
                get
                {
                    string key = "TitleStyle";
                    var style = Styles[key];

                    if (style == null)
                    {
                        style = new GUIStyle(GUI.skin.label);

                        Font lFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                        if (lFont == null) { lFont = EditorStyles.standardFont; }

                        style.font = lFont;
                        style.fontSize = 14;
                        style.fontStyle = FontStyle.Bold;
                        style.normal.textColor = titleTextColor;
                        style.fixedHeight = 30f;
                        style.alignment = TextAnchor.MiddleLeft;
                        style.padding = new RectOffset(0, 0, 0, 0);

                        Styles[key] = style;
                    }
                    return style;
                }
            }
            GUIStyle inspectorBigTitleStlye
            {
                get
                {
                    string key = "BigTitleStyle";
                    var style = Styles[key];

                    if (style == null)
                    {
                        style = new GUIStyle(inspectorTitleStyle);

                        style.fontSize = 30;
                        style.fontStyle = FontStyle.Bold;
                        style.fixedHeight = 60f;

                        Styles[key] = style;
                    }
                    return style;
                }
            }

            public void Draw(string title, string description, bool big = false)
            {
                GUIStyle background = titleBackgroundStyle;
                GUIStyle icon = iconStyle;
                GUIStyle titleStyle = (big) ? inspectorBigTitleStlye : inspectorTitleStyle;

                Vector2 inspectorTitleSize = Vector2.zero;
                float w = icon.normal.background.width;
                float h = icon.normal.background.height;
                inspectorTitleSize.y = titleStyle.fixedHeight;
                inspectorTitleSize.x = (inspectorTitleSize.y / h) * w;
       
                EditorGUILayout.BeginHorizontal(background);
                EditorGUILayout.LabelField("", icon, GUILayout.Width(inspectorTitleSize.x), GUILayout.Height(inspectorTitleSize.y));
                GUILayout.Space(5);
                GUIContent label = new GUIContent(title);

                var size = titleStyle.CalcSize(label);
                EditorGUILayout.LabelField(label, titleStyle, GUILayout.Width(size.x));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (description != "")
                {
                    Color lGUIColor = GUI.color;
                    GUI.color = titleBackground.gamma;
                    EditorGUILayout.HelpBox(description, MessageType.None);
                    GUI.color = lGUIColor;
                }

                EditorGUILayout.Space();
            }
        }
    }
    #endif
}