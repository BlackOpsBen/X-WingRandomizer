using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCards : MonoBehaviour
{
    private CardRandomizer cardRandomizer;
    private DisplayCards displayCards;

    [SerializeField] public int[] mustRemoveAddonTypes { get; private set; }

    public bool mustRemoveMaul { get; private set; }

    private void Awake()
    {
        cardRandomizer = GetComponent<CardRandomizer>();
        displayCards = GetComponent<DisplayCards>();
        mustRemoveMaul = false;
    }

    private void Start()
    {
        ResetMustRemoveList();
    }

    public void ResetMustRemoveList()
    {
        mustRemoveAddonTypes = new int[AddonCardManager.Instance.GetNumAddonTypes() + 2]; // +2 is to account for "Modification costing 3 or less" as different from "Modification" and "Titles"
    }

    public void RemoveAddon(int index)
    {
        AddonCard cardToRemove = cardRandomizer.addonCards[index];

        int type = cardToRemove.GetThisTypeIndex();
        mustRemoveAddonTypes[type]--;

        int[] cardsSlotGrantings = cardToRemove.GetSlotGrantings();

        for (int i = 0; i < cardsSlotGrantings.Length; i++)
        {
            mustRemoveAddonTypes[i] += cardsSlotGrantings[i];
        }

        if (cardToRemove.GetName() == "Vaksai")
        {
            cardRandomizer.allUpgradesAreMinus1 = false;
        }

        if (cardToRemove.GetName() == "TIEx1")
        {
            cardRandomizer.nextSystemIsMinus4 = false;
        }

        if (cardToRemove.GetName() == "Renegade Refit")
        {
            cardRandomizer.allElitesAreMinus1 = false;
        }

        if (cardToRemove.GetName() == "Ezra Bridger" && cardRandomizer.PreviousCardIs("Maul"))
        {
            mustRemoveMaul = true;
        }

        if (cardToRemove.GetName() == "Maul")
        {
            mustRemoveMaul = false;
        }

        cardRandomizer.addonCards.RemoveAt(index);
        displayCards.ClearSingleCard(index);
        cardRandomizer.SetCostModifiers();
        cardRandomizer.CalculateTotalCost();
        UIManager.Instance.UpdateUI();
    }
}
