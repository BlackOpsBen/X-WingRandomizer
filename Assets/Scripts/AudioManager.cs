using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sounds")]
    public Sound[] sounds;

    private bool isReadyToPlay = false;

    private void Awake()
    {
        SingletonPattern();
        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Play(string clipName)
    {
        if (isReadyToPlay)
        {
            Sound s = Array.Find(sounds, sound => sound.name == clipName);
            s.source.Play();
        }
    }

    public void SetIsReadyToPlay()
    {
        isReadyToPlay = true;
    }
}
