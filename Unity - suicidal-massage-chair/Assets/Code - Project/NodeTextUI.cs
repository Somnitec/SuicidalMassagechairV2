using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeTextUI : MonoBehaviour
{
    
    public TextMeshProUGUI Text;

    public NodeGraph Graph => SettingsHolder.Instance.Settings.Graph;
    
    void Update()
    {
        var text = "";

        DialogueNode dialogueNode = Graph.Current as DialogueNode;
        if (dialogueNode != null)
            text += $"{dialogueNode.Data.Data.Text}";

        if (Text != null)
            Text.text = text;
    }
}
