using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject generateUI;

    [SerializeField] private GameObject keepOrPassUI;

    [SerializeField] private float UIToggleDelay = 1f;

    private PointsCounter pointsCounter;

    private void Awake()
    {
        SingletonPattern();

        pointsCounter = GetComponent<PointsCounter>();
    }

    public void EnableGenerate()
    {
        StartCoroutine(DelayEnableGenerate());
    }

    public void EnableKeepOrPass()
    {
        StartCoroutine(DelayEnableKeepOrPass());
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

    private IEnumerator DelayEnableGenerate()
    {
        keepOrPassUI.SetActive(false);
        yield return new WaitForSeconds(UIToggleDelay);
        generateUI.SetActive(true);
    }

    private IEnumerator DelayEnableKeepOrPass()
    {
        generateUI.SetActive(false);
        yield return new WaitForSeconds(UIToggleDelay);
        keepOrPassUI.SetActive(true);
    }

    // So far, called when Squadrons Calculates total cost, and when PilotCardManager changes selected faction.
    public void UpdateUI()
    {
        pointsCounter.UpdatePoints();
    }
}
