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
            string[] productInclusions = pilots[i].GetProductsIncludedWith();
            if (productInclusions != null)
            {
                for (int j = 0; j < productInclusions.Length; j++)
                {
                    bool isValidName = false;
                    for (int k = 0; k < products.Length; k++)
                    {
                        if (productInclusions[j] == products[k].name)
                        {
                            isValidName = true;
                        }
                    }
                    if (!isValidName)
                    {
                        Debug.LogError(productInclusions[j] + " is invalid product name on " + pilots[i].GetType().Name + " titled \'" + pilots[i].name + "\'");
                    }
                }
            }
            else
            {
                Debug.LogError(pilots[i].name + " does not have a product. List products in which it is included.");
            }
            
        }
    }

    private void ValidateAddonProductStrings()
    {
        for (int i = 0; i < addonCards.Length; i++)
        {
            Debug.Log(addonCards[i].name + " test A");
            string[] productInclusions = addonCards[i].GetProductsIncludedWith();
            if (productInclusions != null)
            {
                Debug.Log(productInclusions.Length + " test B");
                for (int j = 0; j < productInclusions.Length; j++)
                {
                    bool isValidName = false;
                    for (int k = 0; k < products.Length; k++)
                    {
                        if (productInclusions[j] == products[k].name)
                        {
                            isValidName = true;
                        }
                    }
                    if (!isValidName)
                    {
                        Debug.LogError(productInclusions[j] + " is invalid product name on " + addonCards[i].GetType().Name + " titled \'" + addonCards[i].name + "\'");
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
