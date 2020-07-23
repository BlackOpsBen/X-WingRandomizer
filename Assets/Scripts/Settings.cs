using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    [SerializeField] private int pointLimit = 100;

    private void Awake()
    {
        SingletonPattern();
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

    public void SetPointLimit(int points)
    {
        pointLimit = points;
    }

    public void SetPointLimit(string text)
    {
        int pointsEntered = int.Parse(text);

        Debug.Log(pointsEntered.ToString() + " is what was parsed.");

        if (pointsEntered > 0)
        {
            pointLimit = pointsEntered;
        }

        UIManager.Instance.UpdateUI();
    }
}
