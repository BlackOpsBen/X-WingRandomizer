using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTrackInfo : MonoBehaviour
{
    [SerializeField] private GameObject trackInfos;

    private float infoTimer = 0f;

    private float timerLimit = 5f;

    public void ShowTrackInfo()
    {
        trackInfos.SetActive(true);
        infoTimer = 0f;
    }

    private void Update()
    {
        infoTimer += Time.deltaTime;

        if (infoTimer > timerLimit)
        {
            HideTrackInfo();
        }
    }

    private void HideTrackInfo()
    {
        trackInfos.SetActive(false);
    }
}
