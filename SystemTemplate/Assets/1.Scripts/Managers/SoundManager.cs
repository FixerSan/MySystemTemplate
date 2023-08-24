using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SoundManager 
{
    private Transform sourceTransform;
    public Transform SourceTransform
    {
        get
        {
            if(sourceTransform == null)
            {
                GameObject go = GameObject.Find("@SoundSources");
                if(go == null)
                    go = new GameObject(name : "@SoundSources");
                sourceTransform = go.transform;
            }
            return sourceTransform;
        }
    }

    private AudioSource bgmSource;
    private List<AudioSourceController> effectSourceController = new List<AudioSourceController>();
    public float bgmVolume;
    public float effectVolume;

    public void SetBGMVolume(float _volume)
    {
        bgmVolume = _volume;
        bgmSource.volume = bgmVolume;
    }

    public void SetEffectVolume(float _volume)
    {
        effectVolume = _volume;
        for (int i = 0; i < effectSourceController.Count; i++)
        {
            effectSourceController[i].SetVoulme(effectVolume);
        }
    }

    public void PlaySoundEffect(SoundProfile_Effect _profileName ,int index = -1)
    {
        string loadKey = _profileName.ToString();
        Managers.Resource.Load<SoundProfile>(loadKey, (soundProfile) => 
        {
            AudioClip audioClip;
            if (index == -1)    audioClip = soundProfile.PlaySoundToRandom();
            else                audioClip = soundProfile.PlaySoundToIndex(index);
        });
    }

    public void StopSoundEffect(AudioSourceController _audioSourceController)
    {
        if(effectSourceController.Count > 1)
        {
            effectSourceController.Find(_audioSourceController);
        }
    }
}
