using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeSystem.Nodes
{
    public class NodeMultiLanguageData
    {
        [ShowInInspector, PropertyOrder(0)]
        private Language language => settings.Language;

        [TabGroup("English"), InlineProperty, HideLabel, HideReferenceObjectPicker, ShowInInspector, SerializeField]
        private NodeData english = new NodeData();

        [TabGroup("Dutch"), InlineProperty, HideLabel, HideReferenceObjectPicker, ShowInInspector, SerializeField]
        private NodeData dutch = new NodeData();
        
        private Settings settings => SettingsHolder.Instance.Settings;
        
        public NodeData Data => settings.Language == Language.Dutch ? dutch : english;

    }
}