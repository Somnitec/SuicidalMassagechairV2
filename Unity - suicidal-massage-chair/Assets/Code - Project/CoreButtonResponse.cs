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
        Events.Instance.AddListener<UserInputUp>(HandleInterruptingInput);
    }

    private void HandleInterruptingInput(UserInputUp userInputUp)
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
            case UserInputButton.Settings:
                SettingsNode();
                break;
        }
    }

    private void SettingsNode()
    {
        graph.PlaySpecialNode(Settings.SettingsNodeName);
    }

    private void SetLanguageAndRestart(Language language)
    {
        settings.Language = language;
        graph.PlayNode(graph.Current);
    }

    private void Kill()
    {
        graph.PlaySpecialNode(Settings.KillNodeName);
    }

    private void Repeat()
    {
        Debug.Log("Repeat");
        settings.Graph.PlayNode(settings.Graph.Current);
    }
}