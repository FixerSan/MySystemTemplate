using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProfile : ScriptableObject
{
    public List<AudioClip> audios;
    public AudioClip PlaySoundToRandom()
    {
        return audios.Random();
    }

    public AudioClip PlaySoundToIndex(int _index)
    {
        return audios.TryGetValue(_index);
    }
}
