using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Song Options")]
    public Sound[] songs;

    private DisplayTrackInfo displayTrackInfo;
    [SerializeField] private ToggleMuteIcon toggleMuteIcon;
    private bool isMuted;

    private List<int> trackNums = new List<int>();
    [SerializeField] private int[] randomizedTrackNums;

    private int currentTrack = 0;

    private void Awake()
    {
        displayTrackInfo = GetComponent<DisplayTrackInfo>();
        CreateAudioSources();
        CreateOrderedTrackNums();
        CreateRandomizedTrackNums();
        PlayNextTrack();
    }

    private void Start()
    {
        LoadMuteSetting();
    }

    private void Update()
    {
        if (!songs[randomizedTrackNums[currentTrack]].source.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void LoadMuteSetting()
    {
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            isMuted = true;
        }
        else
        {
            isMuted = false;
        }

        SetAudioMuteState();
    }

    private void CreateRandomizedTrackNums()
    {
        randomizedTrackNums = new int[songs.Length];
        for (int i = 0; i < songs.Length; i++)
        {
            int rand = UnityEngine.Random.Range(0, trackNums.Count);
            randomizedTrackNums[i] = trackNums[rand];
            trackNums.RemoveAt(rand);
        }
    }

    private void CreateOrderedTrackNums()
    {
        for (int i = 0; i < songs.Length; i++)
        {
            trackNums.Add(i);
        }
    }

    private void CreateAudioSources()
    {
        foreach (Sound s in songs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public void PlayNextTrack()
    {
        songs[randomizedTrackNums[currentTrack]].source.Stop();

        currentTrack++;
        currentTrack = currentTrack % randomizedTrackNums.Length;

        songs[randomizedTrackNums[currentTrack]].source.Play();

        displayTrackInfo.ShowTrackTitle(songs[randomizedTrackNums[currentTrack]].name);
        displayTrackInfo.ShowTrackInfo();
    }

    // Called by UI Button
    public void ToggleMute()
    {
        isMuted = !isMuted;

        SetAudioMuteState();

        SaveMuteSetting();
    }

    private void SetAudioMuteState()
    {
        foreach (Sound song in songs)
        {
            song.source.mute = isMuted;
        }

        toggleMuteIcon.ToggleIcon(isMuted);

        if (!isMuted)
        {
            displayTrackInfo.ShowTrackInfo();
        }
        else
        {
            displayTrackInfo.HideTrackInfo();
        }
    }

    private void SaveMuteSetting()
    {
        int keyValue;

        if (isMuted)
        {
            keyValue = 1;
        }
        else
        {
            keyValue = 0;
        }
        PlayerPrefs.SetInt("Muted", keyValue);
        PlayerPrefs.Save();
    }
}
