using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVerifyProducts : MonoBehaviour
{
    private Product[] products;

    private PilotCard[] pilots;

    private AddonCard[] addonCards;

    private void Awake()
    {
        LoadAllResources();
        LogQuantitiesFound();
        ValidatePilotProductStrings();
        ValidateAddonProductStrings();
    }

    private void LogQuantitiesFound()
    {
        Debug.Log(products.Length + " products found.");
        Debug.Log(pilots.Length + " pilots cards found.");
        Debug.Log(addonCards.Length + " addon cards found.");
    }

    private void LoadAllResources()
    {
        products = Resources.LoadAll<Product>("Products");

        pilots = Resources.LoadAll<PilotCard>("Pilots");

        addonCards = Resources.LoadAll<AddonCard>("");
    }

    private void ValidatePilotProductStrings()
    {
        for (int i = 0; i < pilots.Length; i++)
        {
            for (int j = 0; j < pilots[i].GetProductsIncludedWith().Length; j++)
            {
                bool isValidName = false;
                for (int k = 0; k < products.Length; k++)
                {
                    if (pilots[i].GetProductsIncludedWith()[j] == products[k].name)
                    {
                        isValidName = true;
                    }
                }
                if (!isValidName)
                {
                    Debug.LogError(pilots[i].GetProductsIncludedWith()[j] + " is invalid product name on " + pilots[i].GetType().Name + " titled \'" + pilots[i].name + "\'");
                }
            }
        }
    }

    private void ValidateAddonProductStrings()
    {
        for (int i = 0; i < addonCards.Length; i++)
        {
            if (addonCards[i].GetProductsIncludedWith() != null)
            {
                for (int j = 0; j < addonCards[i].GetProductsIncludedWith().Length; j++)
                {
                    bool isValidName = false;
                    for (int k = 0; k < products.Length; k++)
                    {
                        if (addonCards[i].GetProductsIncludedWith()[j] == products[k].name)
                        {
                            isValidName = true;
                        }
                    }
                    if (!isValidName)
                    {
                        Debug.LogError(addonCards[i].GetProductsIncludedWith()[j] + " is invalid product name on " + addonCards[i].GetType().Name + " titled \'" + addonCards[i].name + "\'");
                    }
                }
            }
            else
            {
                Debug.LogWarning(addonCards[i].name + " needs a product inclusion!");
            }
            
        }
    }
}
