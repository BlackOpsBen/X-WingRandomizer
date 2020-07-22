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

        ValidateProductStrings(pilots);
        ValidateProductStrings(addonCards);
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

    private void ValidateProductStrings(IComeInProducts[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            List<Product> productInclusions = items[i].GetProductsIncludedWith();
            if (productInclusions != null && productInclusions.Count > 0)
            {
                for (int j = 0; j < productInclusions.Count; j++)
                {
                    bool isValidName = false;
                    for (int k = 0; k < products.Length; k++)
                    {
                        if (productInclusions[j].name == products[k].name)
                        {
                            isValidName = true;
                        }
                    }
                    if (!isValidName)
                    {
                        Debug.LogError("\"" + productInclusions[j].name + "\" is invalid product name on " + items[i].GetType().Name + " titled \'" + items[i].GetName() + "\'");
                    }
                }
            }
            else
            {
                Debug.LogWarning(items[i].GetName() + " needs a product inclusion!");
            }

        }
    }
}
