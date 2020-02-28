using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using UnityEngine;

public class RestartOnTimeOut : SingletonMonoBehavior<RestartOnTimeOut>
{
    [ShowInInspector, ReadOnly]
    private float timeSinceLastRestart = 0f;
    private Settings settings => SettingsHolder.Instance.Settings;
    [ShowInInspector, ReadOnly]
    private float timeout => timeSinceLastRestart + settings.TimeOutTimeInSeconds;
    [ShowInInspector, ReadOnly]
    private float now => Time.timeSinceLevelLoad;
    [ShowInInspector, ReadOnly]
    private bool canRestart = false;
    [ShowInInspector, ReadOnly]
    public float TimeRemaining => canRestart ? timeout - now : 0;

    void Start()
    {
        timeSinceLastRestart = now;
        canRestart = true;
        Events.Instance.AddListener<ApplicationStateChange>(ListenToPlaying);
    }

    private void ListenToPlaying(ApplicationStateChange e)
    {
        if (e.State == ApplicationState.Playing)
        {
            timeSinceLastRestart = now;
            canRestart = true;
        }
    }

    void Update()
    {
        if (now > timeout && canRestart)
        {
            canRestart = false;
            Events.Instance.Raise(new StoryFinished());
        }
    }
}
