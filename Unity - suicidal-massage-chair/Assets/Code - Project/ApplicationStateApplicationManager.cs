using System.Collections;
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
        this.State = ApplicationState.Restarting;
        AudioManager.instance.Stop();

        StartCoroutine(logic.InvokeFunctionsCoroutine(settings.RestartChair, StartWaiting));
    }

    private void StartWaiting()
    {
        this.State = ApplicationState.Waiting;

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
        this.State = ApplicationState.Starting;

        Events.Instance.RemoveListener<UserInputUp>(StartStory);
        StopAllCoroutines();

        StartCoroutine(logic.InvokeFunctionsCoroutine(settings.OnStart, PlayRootNode));
    }

    public void PlayRootNode()
    {
        this.State = ApplicationState.Playing;

        settings.Graph.PlayRoot();
    }
}

public enum ApplicationState
{
    Playing,
    Restarting,
    Waiting,
    Starting,
}

public class StoryFinished : Event
{

}
