using System.Collections;
using System.Collections.Generic;
using Framework;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpecialButtonResponse : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private NodeGraph graph => settings.Graph;

    private void Start()
    {
        Events.Instance.AddListener<SpecialInput>(HandleSpecialInput);
    }

    private void HandleSpecialInput(SpecialInput special)
    {
        switch (special.Input)
        {
            case SpecialInputButtons.Repeat:
                Repeat();
                break;
            case SpecialInputButtons.Kill:
                Kill();
                break;
            case SpecialInputButtons.English:
                SetLanguageAndRestart(Language.English);
                break;
            case SpecialInputButtons.Dutch:
                SetLanguageAndRestart(Language.Dutch);
                break;
            case SpecialInputButtons.Settings:
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
        settings.Graph.PlayNode(settings.Graph.Current);
    }
}