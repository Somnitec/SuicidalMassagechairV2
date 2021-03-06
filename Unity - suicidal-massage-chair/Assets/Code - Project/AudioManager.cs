﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    public AudioSource Source;

    public AudioListener Listener;

    public AudioMixer Mixer;

    [OnValueChanged("SetVolume")] [Range(0.001f, 1)]
    public float Volume = 1.0f;
    [ReadOnly, SerializeField]
    private float decibel = 0f;
    
    double clipDuration => (Source.clip != null) ? (double) Source.clip.samples / Source.clip.frequency : 0.0;
    public string ClipProgress => $"[{Source.time:F2}/{clipDuration:F2}]";

    void Start()
    {
        if (Source == null)
            Source = gameObject.AddComponent<AudioSource>();
        if (Listener == null)
            Listener = gameObject.AddComponent<AudioListener>();
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void PlayClip(AudioClip clip, Action onFinished, float invokeOnFinishedFasterInSeconds)
    {
        StopAllCoroutines();
        
        if (clip == null)
        {
            onFinished.Invoke();
            return;
        }

        // TODO crossfade if still playing
        Source.clip = clip;
        Source.Play();

        StartCoroutine(WaitTillFinished(onFinished, invokeOnFinishedFasterInSeconds));
    }

    private IEnumerator WaitTillFinished(Action onFinished, float invokeOnFinishedFasterInSeconds)
    {
        Debug.Log($" {Source.clip.name} {clipDuration:F2} {Source.isPlaying}");
        
        while (Source.isPlaying)
        {
            if (Source.time + invokeOnFinishedFasterInSeconds > clipDuration)
            {
                break;
            }
            yield return null;
        }
        
        yield return new WaitWhile (()=> Source.isPlaying);

        onFinished.Invoke();
    }

    public void PlayEffect(AudioClip fx)
    {
        if (fx == null) return;

        Source.PlayOneShot(fx);
    }

    [Button]
    public void SetVolume(float settingsVolumeLevel)
    {
        Volume = Mathf.Clamp01(settingsVolumeLevel);

        if (Mixer != null)
        {
            decibel = Mathf.Log10(Volume) * 20;
            
            var result = Mixer.SetFloat(Settings.MasterVolumeName, decibel);
            if (!result)
            {
                Debug.LogError($"Cannot set volume of {Mixer}");
            }
        }
    }
}