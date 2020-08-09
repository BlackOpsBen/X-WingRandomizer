using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject generateUI;

    [SerializeField] private GameObject keepOrPassUI;

    [SerializeField] private TextMeshProUGUI keepLabel;

    [SerializeField] private float UIToggleDelay = 1f;

    private PointsCounter pointsCounter;

    private DisableGenerate disableGenerate;

    private DisableKeep disableKeep;

    [SerializeField] private FactionIcon factionIcon;

    private void Awake()
    {
        SingletonPattern();

        pointsCounter = GetComponent<PointsCounter>();

        disableGenerate = GetComponent<DisableGenerate>();

        disableKeep = GetComponent<DisableKeep>();
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

    // So far, called when Squadrons Calculates total cost, and when PilotCardManager changes selected faction, and when making changes to the Product Selections
    public void UpdateUI()
    {
        pointsCounter.UpdatePoints();
        disableGenerate.SetGenerateButton();
        disableKeep.SetKeepButton();
        factionIcon.SetIcon();
    }

    public void DisplaySetCost(int cost)
    {
        keepLabel.text = "KEEP (" + cost.ToString() + "pts)";
    }
}
