﻿using System.Collections;
using System.Collections.Generic;
using Framework;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Event = Framework.Event;

public class ApplicationStateApplicationManager : SingletonMonoBehavior<ApplicationStateApplicationManager>
{
    [ReadOnly] public ApplicationState State = ApplicationState.Playing;

    private Settings settings => SettingsHolder.Instance.Settings;
    private NodePlayingLogic _playingLogic = new NodePlayingLogic();
    public bool NotPlaying => State != ApplicationState.Playing;

    void Start()
    {
        Events.Instance.AddListener<StoryFinished>(RestartApplication);

        if (settings.ResetChairOnStart)
            RestartApplication();
        else
        {
            ResetValues();
            ChangeState(ApplicationState.Playing);
        }
    }

    [Button]
    public void RestartApplication()
    {
        RestartApplication(new StoryFinished());
    }

    private void RestartApplication(StoryFinished storyFinished)
    {
        ChangeState(ApplicationState.Restarting);
        
        AudioManager.Instance.Stop();
        ResetValues();

        StartCoroutine(_playingLogic.InvokeFunctionsCoroutine(settings.RestartChair, StartWaiting, this));
    }

    private void ResetValues()
    {
        settings.Graph.Reset();
        Events.Instance.Raise(new ResetValuesAfterRestart());
    }

    private void StartWaiting()
    {
        ChangeState(ApplicationState.Waiting);
        
        Events.Instance.Raise(new CustomButtonTextUpdate(
            settings.WaitingCustomButtonA,
            settings.WaitingCustomButtonB,
            settings.WaitingCustomButtonC
        ));
        
        Events.Instance.AddListener<AllInput>(StartStory);
        Waiting();
    }

    private void Waiting()
    {
        _playingLogic.PlayFunctionsAndAudio(
            Waiting,
            settings.WaitingAudio,
            settings.WaitingFunctions,
            "Waiting", NodeFunctionRunner.Instance);
    }

    private void StartStory(AllInput e)
    {
        ChangeState(ApplicationState.Starting);

        Events.Instance.RemoveListener<AllInput>(StartStory);
        StopAllCoroutines();

        StartCoroutine(_playingLogic.InvokeFunctionsCoroutine(settings.OnStart, PlayRootNode, this));
    }

    public void PlayRootNode()
    {
        ChangeState(ApplicationState.Playing);

        settings.Graph.PlayRoot();
        
        StartCoroutine(_playingLogic.InvokeFunctionsCoroutine(settings.AfterStart, Empty, this));
    }

    private void Empty()
    {
        
    }

    private void ChangeState(ApplicationState state)
    {
        this.State = state;

        Events.Instance.Raise(new ApplicationStateChange(state));
    }
}

public enum ApplicationState
{
    Playing,
    Restarting,
    Waiting,
    Starting,
}

public class ApplicationStateChange : Event
{
    public ApplicationState State;

    public ApplicationStateChange(ApplicationState state)
    {
        this.State = state;
    }
}

public class StoryFinished : Event
{
}