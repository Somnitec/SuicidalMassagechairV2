namespace Runemark.VisualEditor
{
    using System.Collections.Generic;
    using UnityEngine;
    using Runemark.Common;

    public class VisualEditorGUIStyle
    {
        public struct TextureInfo { public Texture2D Texture; public RectOffset BorderOffset; }
        static Dictionary<string, TextureInfo> _textures = new Dictionary<string, TextureInfo>();
        public static TextureInfo GetTexture(Color border, Color fill, bool tl, bool tr, bool bl, bool br)
        {
            string key = border + "" + fill + "" + tl + "" + tr + "" + bl + "" + br;
            if (_textures.ContainsKey(key))
                return _textures[key];
            return CreateTexture(key, border, fill, tl, tr, bl, br);
        }
        static TextureInfo CreateTexture(string key, Color border, Color fill, bool tl, bool tr, bool bl, bool br)
        {
            var tex = new RectangleTexture()
            {
                BorderColor = border,
                FillColor = fill,
                Resolution = 512,
                CornerRadius = 10,
                BorderThickness = 1,
                TL_isRounded = tl,
                TR_isRounded = tr,
                BL_isRounded = bl,
                BR_isRounded = br
            };
            var t = new TextureInfo()
            {
                BorderOffset = tex.BorderOffset,
                Texture = tex.Generate()
            };

            _textures.Add(key, t);
            return t;
        }

        static Dictionary<string, GUIStyle> _styles = new Dictionary<string, GUIStyle>();

        public static GUIStyle WindowHeader(TextAnchor align, int size = 16, bool isLink = false)
        {
            string key = "WindowHeader" + align + size+isLink;
            if (!_styles.ContainsKey(key))
            { 
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.alignment = align;
                style.fontSize = size;
                style.fontStyle = FontStyle.Bold;

                if (isLink)
                {
                    style.hover.textColor = Color.blue;
                }

                _styles.Add(key, style);
            }
            return _styles[key];
        }
        public static GUIStyle Box()
        {
            string key = "Box";
            if (!_styles.ContainsKey(key))
            {
                GUIStyle style = new GUIStyle(GUI.skin.box);
                _styles.Add(key, style);
            }
            return _styles[key];
        }
        public static GUIStyle ColorBox(Color color)
        {
            string key = "ColorBox"+color;
            if (!_styles.ContainsKey(key))
            {
                var tex = GetTexture(color, color, false, false, false, false);

                GUIStyle style = new GUIStyle(GUI.skin.box);
                style.normal.background = tex.Texture;
                style.border = tex.BorderOffset;
                _styles.Add(key, style);
            }
            return _styles[key];           
        }
        public static GUIStyle DiscreteButtonList(bool selected = false)
        {
            string key = "DiscreteButtonList"+selected;
            if (!_styles.ContainsKey(key))
            {
                GUIStyle style = new GUIStyle(GUI.skin.box);
                style.alignment = TextAnchor.MiddleLeft;
                style.fontStyle = selected ? FontStyle.Bold : FontStyle.Normal;

                _styles.Add(key, style);
            }
            return _styles[key];
        }

    }
}