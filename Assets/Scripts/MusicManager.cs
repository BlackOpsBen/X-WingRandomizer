using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Song Options")]
    public Sound[] songs;

    private DisplayTrackInfo displayTrackInfo;

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

    private void Update()
    {
        if (!songs[randomizedTrackNums[currentTrack]].source.isPlaying)
        {
            PlayNextTrack();
        }
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
        Debug.Log("Playing track #" + randomizedTrackNums[currentTrack].ToString());
        songs[randomizedTrackNums[currentTrack]].source.Play();
        displayTrackInfo.ShowTrackInfo();
    }
}
