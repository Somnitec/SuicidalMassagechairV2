﻿using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using Event = Framework.Event;

public class ApplicationStateApplicationManager : SingletonMonoBehavior<ApplicationStateApplicationManager>
{
    [ReadOnly]
    public ApplicationState State = ApplicationState.Playing;

    private Settings settings => SettingsHolder.Instance.Settings;
    private NodeLogic logic = new NodeLogic();

    void Start()
    {
        Events.Instance.AddListener<StoryFinished>(RestartApplication);

        if (settings.ResetChairOnStart)
            RestartApplication();
        else
            ChangeState(ApplicationState.Playing);


        // Example of using this event on the End node
        // Events.Instance.Raise(new StoryFinished());
    }

    [Button]
    public void RestartApplication()
    {
        RestartApplication(new StoryFinished());
    }

    private void RestartApplication(StoryFinished storyFinished)
    {
        ChangeState(ApplicationState.Restarting);
        AudioManager.instance.Stop();

        StartCoroutine(logic.InvokeFunctionsCoroutine(settings.RestartChair, StartWaiting));
    }

    private void StartWaiting()
    {
        ChangeState(ApplicationState.Waiting);

        Events.Instance.AddListener<UserInputUp>(StartStory);
        Waiting();
    }

    private void Waiting()
    {
        StartCoroutine(logic.InvokeFunctionsAndPlayAudioCoroutine(
            "Waiting",
            settings.WaitingAudio,
            settings.WaitingFunctions,
            Waiting));
    }

    private void StartStory(UserInputUp e)
    {
        ChangeState(ApplicationState.Starting);

        Events.Instance.RemoveListener<UserInputUp>(StartStory);
        StopAllCoroutines();

        StartCoroutine(logic.InvokeFunctionsCoroutine(settings.OnStart, PlayRootNode));
    }

    public void PlayRootNode()
    {
        ChangeState(ApplicationState.Playing);

        settings.Graph.PlayRoot();
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
