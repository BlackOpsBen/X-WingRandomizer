using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ship : Item, IComeInProducts
{
    [SerializeField] private List<Product> includedWith = new List<Product>();

    [Header("Stats")]
    public int shieldValue;
    public int hullValue;

    [Header("Action Bar")]
    public bool focus;
    public bool targetLock;
    public bool boost;
    public bool evade;
    public bool barrelRoll;
    public bool cloak;
    public bool slam;
    public bool reinforce;

    [Header("Faction")]
    public bool rebel;
    public bool imperial;
    public bool scum;

    [Header("Ship class/size/type")]
    public bool smallShip;
    public bool largeShip;
    public bool hugeShip;
    public bool heavyTIE;
    public bool TIE;

    [Header("Title Options")]
    [SerializeField] private Title[] titles;

    private int cheapestPilotCost = 100;

    public void SetCheapestPilotCost(int cost)
    {
        cheapestPilotCost = cost;
    }

    public int GetCheapestPilotCost()
    {
        return cheapestPilotCost;
    }

    public List<Product> GetProductsIncludedWith()
    {
        return includedWith;
    }

    public string GetName()
    {
        return name;
    }

    public void ReciprocateInclusion(Product product)
    {
        includedWith.Add(product);
    }

    public int GetProductListCount()
    {
        return includedWith.Count;
    }

    public Title[] GetTitleOptions()
    {
        return titles;
    }
}
