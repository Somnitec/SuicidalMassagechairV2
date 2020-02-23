using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class GraphUIController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public NodeGraph Graph => SettingsHolder.Instance.Settings.Graph;
    private AudioManager audMgr => AudioManager.Instance;
    private ApplicationStateApplicationManager appMgr => ApplicationStateApplicationManager.Instance;
    void Update()
    {
        var text = $"Application info\n" +
                   $"State: {appMgr.State}\n" +
                   $"Node: {Graph.Current.name}\n" +
                   $"Audio: {audMgr.Source.clip.name}\n" +
                   $"Duration: {audMgr.ClipProgress}\n" +
                   $"Playing: {audMgr.Source.isPlaying}\n" ;

        DialogueNode dialogueNode = Graph.Current as DialogueNode;
        if (dialogueNode != null)
            text += $"FunctionsFinished: {dialogueNode.Logic.FunctionsFinished} \n" +
                    $"AudioFinished: {dialogueNode.Logic.AudioFinished}";

        if (Text != null)
            Text.text = text;
    }
}