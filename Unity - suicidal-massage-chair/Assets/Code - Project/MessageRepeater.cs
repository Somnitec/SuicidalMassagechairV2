using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MessageRepeater : MonoBehaviour
{
    public Messager Messager;
    public int Value = -1;
    
    [Range(0,10f)]
    [InfoBox("How long it waits between each refresh (set to 0 for each update)")]
    public float RefreshTime = 1f;

    [ReadOnly,ShowInInspector]
    private float TimePassedSinceLastRefresh = 0f;
    private Settings settings => SettingsHolder.Instance.Settings;
    
    void Update()
    {
        TimePassedSinceLastRefresh += Time.deltaTime;
        if (TimePassedSinceLastRefresh >= RefreshTime)
        {
            this.Messager.SendMessageToArduino(settings.RepeatedMessage, Value);
            TimePassedSinceLastRefresh = 0;
        }
    }
}
