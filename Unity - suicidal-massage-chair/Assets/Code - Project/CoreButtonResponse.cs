using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoreButtonResponse : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private NodeGraph graph => settings.Graph;

    private void Start()
    {
        Events.Instance.AddListener<UserInputUp>(HandleInput);
    }

    public void HandleInput(UserInputUp userInputUp)
    {
        switch (userInputUp.Button)
        {
            case UserInputButton.Repeat:
                Repeat();
                break;
            case UserInputButton.Kill:
                Kill();
                break;
            case UserInputButton.English:
                SetLanguageAndRestart(Language.English);
                break;
            case UserInputButton.Dutch:
                SetLanguageAndRestart(Language.Dutch);
                break;    
        }
    }

    private void SetLanguageAndRestart(Language language)
    {
        settings.Language = language;
        graph.PlayNode(graph.Current);
    }

    private void Kill()
    {
        graph.PlaySpecialNode("KillNode");
    }

    private void Repeat()
    {
        Debug.Log("Repeat");
        settings.Graph.PlayNode(settings.Graph.Current);
    }
}