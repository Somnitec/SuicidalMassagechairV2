namespace Runemark.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class RunemarkEditor : Editor
    {
        Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();
        protected void FindProperty(string name)
        {
            if (name.Contains("."))
            {
                if (properties.ContainsKey(name)) return;
                var prop = serializedObject.FindProperty(name);
                properties.Add(name, prop);
            }
            else
            {
                string[] names = name.Split('.');
                SerializedProperty prop = serializedObject.FindProperty(names[0]);
                for(int i= 1; i<names.Length; i++)
                {
                    var tempProp = prop.FindPropertyRelative(names[i]);
                    if (tempProp != null) prop = tempProp;
                }
                properties.Add(name, prop);
            }  
        }
        protected void DrawPropertyField(string name, string label = "")
        {
            DrawPropertyField(name, (label != "") ? new GUIContent(label) : GUIContent.none);
        }
        protected void DrawPropertyField(string name, GUIContent label)
        {
            if (!properties.ContainsKey(name)) return;
            var prop = properties[name];
            DrawPropertyField(prop, label);
        }

        protected void DrawPropertyField(SerializedProperty prop, GUIContent label)
        {
            if (label == GUIContent.none) EditorGUILayout.PropertyField(prop, true);
            else EditorGUILayout.PropertyField(prop, label, true);
        }

        protected SerializedProperty GetProperty(string name)
        {
            if (!properties.ContainsKey(name)) return null;
            var prop = properties[name];
            return prop;
        }



        protected void DrawPropertyFieldWithToggle(string name, string enableName, string label = "")
        {
            DrawPropertyField(enableName);
            var prop = GetProperty(enableName);
            if (prop.boolValue) DrawPropertyField(name, label);
            GUILayout.Space(5);
        }
    }
}