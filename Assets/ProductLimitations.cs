using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductLimitations : MonoBehaviour
{
    private List<Product> productsIncluded = new List<Product>();

    private void Awake()
    {
        Product[] productsInResources = Resources.LoadAll<Product>("Resources/Products");

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
        foreach (Product includedProduct in productsIncluded)
        {
            foreach (Product productRequired in item.GetProductsIncludedWith())
            {
                if (productRequired.name == includedProduct.name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
