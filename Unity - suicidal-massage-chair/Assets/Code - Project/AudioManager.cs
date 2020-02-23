using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    public AudioSource Source;

    public AudioListener Listener;

    double clipDuration => (double) Source.clip.samples / Source.clip.frequency;
    public string ClipProgress => $"[{Source.time.ToString("F2")},{clipDuration.ToString("F2")}]";

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

    public void PlayClip(AudioClip clip, Action onFinished)
    {
        if (clip == null)
        {
            onFinished.Invoke();
            return;
        }

        // TODO crossfade if still playing
        Source.clip = clip;
        Source.Play();

        StartCoroutine(WaitTillFinished(onFinished));
    }

    private IEnumerator WaitTillFinished(Action onFinished)
    {
        yield return new WaitForSeconds((float) clipDuration);

        onFinished.Invoke();
    }
}