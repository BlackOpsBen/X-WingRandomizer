using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    [SerializeField] private int pointLimit = 100;

    [SerializeField] private TMP_InputField pointsInputField;

    private void Awake()
    {
        SingletonPattern();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("PointLimit"))
        {
            LoadSetting();
        }
        else
        {
            InitializeSetting();
        }
    }

    private void InitializeSetting()
    {
        int startingValue = 100;
        PlayerPrefs.SetInt("PointLimit", startingValue);
        PlayerPrefs.Save();
        pointsInputField.text = startingValue.ToString();
        pointLimit = 100;
    }

    private void LoadSetting()
    {
        int savedLimit = PlayerPrefs.GetInt("PointLimit");
        pointsInputField.text = savedLimit.ToString();
        pointLimit = savedLimit;
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

    public int GetPointLimit()
    {
        return pointLimit;
    }

    // Called by UI input field OnValueChanged
    public void SetPointLimit(string text)
    {
        int pointsEntered = int.Parse(text);

        if (pointsEntered > 0)
        {
            pointLimit = pointsEntered;

            PlayerPrefs.SetInt("PointLimit", pointLimit);
            PlayerPrefs.Save();
        }

        UIManager.Instance.UpdateUI();
    }
}
