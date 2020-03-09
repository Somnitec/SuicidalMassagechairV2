using System;
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
    private AudioManager audioManager => AudioManager.Instance;

    private void Start()
    {
        Events.Instance.AddListener<SpecialInput>(HandleSpecialInput);
        SetUpVolume();
    }

    [Button]
    private void SetUpVolume()
    {
        if (settings.VolumeLevels == null || settings.VolumeLevels.Count < 5)
        {
            settings.VolumeLevels = new List<float>()
            {
                0.2f,
                0.4f,
                0.6f,
                0.8f,
                1.0f,
            };
        }
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
            case SpecialInputButtons.One:
                Volume(0);
                break;
            case SpecialInputButtons.Two:
                Volume(1);
                break;
            case SpecialInputButtons.Three:
                Volume(2);
                break;
            case SpecialInputButtons.Four:
                Volume(3);
                break;
            case SpecialInputButtons.Five:
                Volume(4);
                break;
            case SpecialInputButtons.Horn:
                break;
            default:
                break;
        }
    }

    private void Volume(int settingIndex)
    {
        audioManager.SetVolume(settings.VolumeLevels[settingIndex]);
    }

    private void SettingsNode()
    {
        if (ApplicationStateApplicationManager.Instance.NotPlaying)
            return;
        
        graph.PlaySpecialNode(Settings.SettingsNodeName);
    }

    private void SetLanguageAndRestart(Language language)
    {
        settings.Language = language;
        
        if (ApplicationStateApplicationManager.Instance.NotPlaying)
            return;
        
        graph.PlayNode(graph.Current);
    }

    private void Kill()
    {
        if (ApplicationStateApplicationManager.Instance.NotPlaying)
            return;
        
        graph.PlaySpecialNode(Settings.KillNodeName);
    }

    private void Repeat()
    {
        if (ApplicationStateApplicationManager.Instance.NotPlaying)
            return;
        
        settings.Graph.PlayNode(settings.Graph.Current);
    }
}