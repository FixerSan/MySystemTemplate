using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController
{
    private AudioSource audioSource;
    public AudioSource AudioSource 
    {
        get
        {
            if (audioSource == null)
                Init();
            return audioSource;
        }
    }

    private void Init()
    {
        audioSource = Managers.Sound.SourceTransform.gameObject.AddComponent<AudioSource>();
        audioSource.Stop();
        audioSource.playOnAwake = false;
        audioSource.volume = Managers.Sound.effectVolume;
    }

    public void Play(AudioClip _audioClip)
    {
        AudioSource.clip = _audioClip;
        Managers.Routine.StartCoroutine(Play());
    }

    public void SetVoulme(float _volume)
    {
        audioSource.volume = _volume;
    }

    public void Clear()
    {
        audioSource.Stop();

    }

    public void Remove()
    {
        Object.Destroy(audioSource);
    }

    private IEnumerator Play()
    {
        AudioSource.Play();
        if(Managers.Sound.list)
    }
}
