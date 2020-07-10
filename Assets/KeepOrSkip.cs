using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOrSkip : MonoBehaviour
{
    private DisplayCards displayCards;

    private void Awake()
    {
        displayCards = GetComponent<DisplayCards>();
    }

    public void KeepSet()
    {
        Debug.Log("Kept");

        // Record the set in Squadrons Manager

        // Hide all Addons
        displayCards.ClearPreviousCards();

        // Flip Pilot Card
        displayCards.FlipPilotCard();

        // Reset the set

        ToggleUI();
    }

    public void SkipSet()
    {
        Debug.Log("Skipped");

        displayCards.ClearPreviousCards();

        displayCards.FlipPilotCard();

        ToggleUI();
    }

    private void ToggleUI()
    {
        UIManager.Instance.EnableGenerate();
    }
}
