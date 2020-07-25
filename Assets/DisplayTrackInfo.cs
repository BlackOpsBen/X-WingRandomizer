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
            HideTrackInfo();
        }
    }

    public void ShowTrackInfo()
    {
        trackInfos.SetActive(true);
        infoTimer = 0f;
    }

    private void HideTrackInfo()
    {
        trackInfos.SetActive(false);
    }

    public void ShowTrackTitle(string title)
    {
        trackNameText.text = title;
    }
}
