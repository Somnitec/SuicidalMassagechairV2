using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class GraphUIController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public NodeGraph Graph => SettingsHolder.Instance.Settings.Graph;
    private AudioManager mgr => AudioManager.Instance;
    void Update()
    {
        var text = $"Node info\n" +
                   $"Playing: {Graph.Current.name}\n" +
                   $"Audio: {mgr.Source.clip.name}\n" +
                   $"Duration: {mgr.ClipProgress}\n" +
                   $"Playing: {mgr.Source.isPlaying}\n" ;

        DialogueNode dialogueNode = Graph.Current as DialogueNode;
        if (dialogueNode != null)
            text += $"FunctionsFinished: {dialogueNode.Logic.FunctionsFinished} \n" +
                    $"AudioFinished: {dialogueNode.Logic.AudioFinished}";

        if (Text != null)
            Text.text = text;
    }
}