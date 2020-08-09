using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableKeep : MonoBehaviour
{
    [SerializeField] private RemoveCards removeCards;
    [SerializeField] private CardRandomizer cardRandomizer;

    [SerializeField] private Button keepButton;
    private TextMeshProUGUI buttonLabel;

    private void Awake()
    {
        buttonLabel = keepButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetKeepButton()
    {
        keepButton.interactable = true;
        cardRandomizer.CalculateTotalCost();

        for (int i = 0; i < removeCards.mustRemoveAddonTypes.Length; i++)
        {
            if (removeCards.mustRemoveAddonTypes[i] > 0 && cardRandomizer.TypeIsSelected(i))
            {
                keepButton.interactable = false;
                buttonLabel.text = "Must remove a " + AddonCardManager.Instance.addonCardNames[i];
            }
        }

        if (removeCards.mustRemoveMaul)
        {
            keepButton.interactable = false;
            buttonLabel.text = "Must remove \"•Maul\"";
        }
    }
}
