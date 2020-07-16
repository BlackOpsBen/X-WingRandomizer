using UnityEngine;

[CreateAssetMenu]
public class Ship : ScriptableObject
{
    [Header("Stats")]
    public int shieldValue;

    [Header("Action Bar")]
    public bool focus;
    public bool targetLock;
    public bool boost;
    public bool evade;
    public bool barrelRoll;
    public bool cloak;
    public bool slam;

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
}
