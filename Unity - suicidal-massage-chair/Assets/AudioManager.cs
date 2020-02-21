using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    public AudioSource Source;

    public AudioListener Listener;

    double clipDuration => (double)Source.clip.samples / Source.clip.frequency;


    void Start()
    {
        if(Source == null)
            Source = gameObject.AddComponent<AudioSource>();
        if(Listener == null)
            Listener = gameObject.AddComponent<AudioListener>();
    }

    public void PlayClip(AudioClip clip, Action onFinished)
    {
        // TODO crossfade if still playing
        Source.clip = clip;
        Source.Play();

        StartCoroutine(WaitTillFinished(onFinished));
    }

    private IEnumerator WaitTillFinished(Action onFinished)
    {
        yield return new WaitForSeconds((float)clipDuration);

        onFinished.Invoke();
    }
}
