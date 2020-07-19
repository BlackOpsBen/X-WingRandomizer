using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComeInProducts
{
    List<Product> GetProductsIncludedWith();

    string GetName();

    void ReciprocateInclusion(Product product);

    int GetProductListCount();
}
