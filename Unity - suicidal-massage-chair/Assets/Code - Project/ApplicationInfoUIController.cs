using System.Collections;
using System.Collections.Generic;
using NodeSystem.Nodes;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ApplicationInfoUIController : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public TextMeshProUGUI NodePlaying;
    public TextMeshProUGUI StateText;

    

    public NodeGraph Graph => SettingsHolder.Instance.Settings.Graph;
    private AudioManager audMgr => AudioManager.Instance;
    private ApplicationStateApplicationManager appMgr => ApplicationStateApplicationManager.Instance;
    private RestartOnTimeOut restartOnTimeOut => RestartOnTimeOut.Instance;

    void Update()
    {
        
        
        var text = $"Application info\n" +
                   $"TimeRemaining: {restartOnTimeOut.TimeRemaining:F}\n" +
                   $"Audio: {audMgr.Source.clip?.name}\n" +
                   $"Audio Duration: {audMgr.ClipProgress}\n" +
                   $"Playing: {audMgr.Source.isPlaying}\n";

        DialogueNode dialogueNode = Graph.Current as DialogueNode;
        if (dialogueNode != null)
            text += $"Function: {dialogueNode.playingLogic.FunctionProgress}\n" +
                    $"FunctionsFinished: {dialogueNode.playingLogic.FunctionsFinished} \n" +
                    $"AudioFinished: {dialogueNode.playingLogic.AudioFinished}";

        if (Text != null)
            Text.text = text;
        
        if (NodePlaying != null)
            NodePlaying.text = Graph.Current?.name;
        
        if (StateText != null)
            StateText.text = appMgr.State.ToString();
    }
}