using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductLimitations : MonoBehaviour
{
    private List<Product> productsIncluded = new List<Product>();

    private void Awake()
    {
        Product[] productsInResources = Resources.LoadAll<Product>("Products");

        for (int i = 0; i < productsInResources.Length; i++)
        {
            productsIncluded.Add(productsInResources[i]);
        }
    }

    public List<Product> GetIncludedProducts()
    {
        return productsIncluded;
    }

    public bool ItemIsAvailable(IComeInProducts item)
    {
        for (int i = 0; i < productsIncluded.Count; i++)
        {
            if (item.GetProductsIncludedWith().Count > 0)
            {
                for (int j = 0; j < item.GetProductsIncludedWith().Count; j++)
                {
                    if (item.GetProductsIncludedWith()[j] != null)
                    {
                        Debug.Log(item.GetProductsIncludedWith()[j].name + " is being compared with " + productsIncluded[i].name);
                        if (item.GetProductsIncludedWith()[j] == productsIncluded[i])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Error state 2.");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Error state 1.");
            }
        }

        return false;
    }
}
