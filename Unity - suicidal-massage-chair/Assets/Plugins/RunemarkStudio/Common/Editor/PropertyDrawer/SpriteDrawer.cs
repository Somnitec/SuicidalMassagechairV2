namespace Runemark.Common
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Sprite))]
    public class SpriteDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.objectReferenceValue = (Sprite)EditorGUI.ObjectField(position, label, (Sprite)property.objectReferenceValue, typeof(Sprite), false);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 50;
        }
    }
}