using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Product : ScriptableObject
{
    [SerializeField] private Item[] itemsIncluded;

    private void OnValidate()
    {
        if (itemsIncluded.Length > 0)
        {
            foreach (IComeInProducts item in itemsIncluded)
            {
                bool needToAskForInclusion = true;

                if (item != null)
                {
                    if (item.GetProductListCount() > 0)
                    {
                        List<Product> itemsProducts = item.GetProductsIncludedWith();

                        foreach (Product product in itemsProducts)
                        {
                            if (product.name == this.name)
                            {
                                //Debug.Log(item.GetType() + " " + item.GetName() + " and " + this.GetType() + " " + this.name + " already reciprocate.");
                                needToAskForInclusion = false;
                            }
                        }
                    }

                    if (needToAskForInclusion)
                    {
                        Debug.LogWarning("Need to ask " + item.GetName() + " to reciprocate inclusion in this product.");
                        item.ReciprocateInclusion(this);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Product " + this.name + " does not list any included items.");
        }
    }
}


/*
 * if (item.GetName() == this.name)
            {
                Debug.Log(item.GetType() + " " + item.GetName() + " and " + this.GetType() + " " + this.name + " already reciprocate.");
            }
*/