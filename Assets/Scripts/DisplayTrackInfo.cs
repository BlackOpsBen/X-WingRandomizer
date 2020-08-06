using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTrackInfo : MonoBehaviour
{
    [SerializeField] private GameObject trackInfos;
    [SerializeField] private TextMeshProUGUI trackNameText;

    private float infoTimer = 0f;

    private float timerLimit = 5f;

    private void Update()
    {
        infoTimer += Time.deltaTime;

        if (infoTimer > timerLimit)
        {
            TimeoutTrackInfo();
        }
    }

    public void ShowTrackInfo()
    {
        trackInfos.SetActive(true);
        infoTimer = 0f;
    }

    public void HideTrackInfo()
    {
        infoTimer = timerLimit;
    }

    private void TimeoutTrackInfo()
    {
        trackInfos.SetActive(false);
    }

    public void ShowTrackTitle(string title)
    {
        trackNameText.text = title;
    }
}
