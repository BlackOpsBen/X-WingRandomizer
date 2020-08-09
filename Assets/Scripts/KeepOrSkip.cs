using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOrSkip : MonoBehaviour
{
    private DisplayCards displayCards;
    private CardRandomizer cardRandomizer;
    private RemoveCards removeCards;

    private void Awake()
    {
        displayCards = GetComponent<DisplayCards>();
        cardRandomizer = GetComponent<CardRandomizer>();
        removeCards = GetComponent<RemoveCards>();
    }

    public void KeepSet()
    {
        // Record the set in Squadrons Manager
        Squadrons.Instance.RecordPilotSet(cardRandomizer.ship, cardRandomizer.pilot, cardRandomizer.addonCards.ToArray(), cardRandomizer.GetCostModifiers());

        // Hide all Addons
        displayCards.ClearPreviousCards();

        // Flip Pilot Card
        displayCards.FlipPilotCard();

        removeCards.ResetMustRemoveList();

        ToggleUI();
    }

    public void SkipSet()
    {
        displayCards.ClearPreviousCards();

        displayCards.FlipPilotCard();

        removeCards.ResetMustRemoveList();

        ToggleUI();
    }

    private void ToggleUI()
    {
        UIManager.Instance.EnableGenerate();
    }
}
