using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCards : MonoBehaviour
{
    private CardRandomizer cardRandomizer;
    private DisplayCards displayCards;

    [SerializeField] public int[] mustRemoveAddonTypes { get; private set; }

    private void Awake()
    {
        cardRandomizer = GetComponent<CardRandomizer>();
        displayCards = GetComponent<DisplayCards>();
    }

    private void Start()
    {
        ResetMustRemoveList();
    }

    public void ResetMustRemoveList()
    {
        mustRemoveAddonTypes = new int[AddonCardManager.Instance.GetNumAddonTypes() + 1]; // +1 is to account for "Modification costing 3 or less" as different from "Modification"
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

        cardRandomizer.addonCards.RemoveAt(index);
        displayCards.ClearSingleCard(index);
        cardRandomizer.CalculateTotalCost();
    }
}
