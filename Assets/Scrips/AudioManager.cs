using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager Instance;
    public AudioSource[] AudioSources;

    void Start()
    {
        Instance = this;
    }

    public void Play(SoundTrack track)
    {
        switch (track)
        {
            case SoundTrack.Explosion:
                AudioSources[0].Play();
                break;
            case SoundTrack.Blip:
                AudioSources[1].Play();
                break;
            case SoundTrack.Jump:
                AudioSources[2].Play();
                break;
            default:
                break;
        }
    }

    public enum SoundTrack
    {
        Explosion,
        Blip,
        Jump
    }
}
