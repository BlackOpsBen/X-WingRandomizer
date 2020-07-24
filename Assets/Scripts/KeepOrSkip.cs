using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOrSkip : MonoBehaviour
{
    private DisplayCards displayCards;
    private CardRandomizer cardRandomizer;

    private void Awake()
    {
        displayCards = GetComponent<DisplayCards>();
        cardRandomizer = GetComponent<CardRandomizer>();
    }

    public void KeepSet()
    {
        // Record the set in Squadrons Manager
        Squadrons.Instance.RecordPilotSet(cardRandomizer.ship, cardRandomizer.pilot, cardRandomizer.addonCards.ToArray(), cardRandomizer.GetCostModifiers());

        // Hide all Addons
        displayCards.ClearPreviousCards();

        // Flip Pilot Card
        displayCards.FlipPilotCard();

        // Reset the set

        ToggleUI();
    }

    public void SkipSet()
    {
        displayCards.ClearPreviousCards();

        displayCards.FlipPilotCard();

        ToggleUI();
    }

    private void ToggleUI()
    {
        UIManager.Instance.EnableGenerate();
    }
}
