using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ApplicationInfoUIController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public NodeGraph Graph => SettingsHolder.Instance.Settings.Graph;
    private AudioManager audMgr => AudioManager.Instance;
    private ApplicationStateApplicationManager appMgr => ApplicationStateApplicationManager.Instance;
    private RestartOnTimeOut restartOnTimeOut => RestartOnTimeOut.Instance;

    void Update()
    {
        var text = $"Application info\n" +
                   $"State: {appMgr.State}\n" +
                   $"TimeRemaining: {restartOnTimeOut.TimeRemaining}\n" +
                   $"Node: {Graph.Current.name}\n" +
                   $"Audio: {audMgr.Source.clip.name}\n" +
                   $"Audio Duration: {audMgr.ClipProgress}\n" +
                   $"Playing: {audMgr.Source.isPlaying}\n";

        DialogueNode dialogueNode = Graph.Current as DialogueNode;
        if (dialogueNode != null)
            text += $"Function: {dialogueNode.Logic.FunctionProgress}\n" +
                    $"FunctionsFinished: {dialogueNode.Logic.FunctionsFinished} \n" +
                    $"AudioFinished: {dialogueNode.Logic.AudioFinished}";

        if (Text != null)
            Text.text = text;
    }
}