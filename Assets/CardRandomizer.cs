using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomizer : MonoBehaviour
{
    public Ship ship;
    public PilotCard pilot;
    public List<AddonCard> addonCards;
    public int totalCost;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            addonCards.Clear();
            totalCost = 0;
            MakeRandomPilot();
            SelectAddons();
        }
    }

    private void MakeRandomPilot()
    {
        int randShip = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelShips.Length);
        ship = PilotCardManager.Instance.rebelShips[randShip];
        Debug.Log(ship.name);

        int randPilot = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelPilotGroups[randShip].pilots.Length);
        pilot = PilotCardManager.Instance.rebelPilotGroups[randShip].pilots[randPilot];
        Debug.Log(pilot.name);

        pilot.MakeList();

        totalCost += pilot.GetCost();
    }

    private void SelectAddons()
    {
        for (int i = 0; i < pilot.GetNumAddonTypes(); i++)
        {
            for (int j = 0; j < pilot.GetAddonTypeQuantity(i); j++)
            {
                // roll to see if to be filled
                if (Roll())
                {
                    int randMax = AddonCardManager.Instance.GetAddonCardGroupLength(i);
                    int rand = UnityEngine.Random.Range(0, randMax);
                    AddonCard selectedCard = AddonCardManager.Instance.GetAddonCard(i, rand);
                    addonCards.Add(selectedCard);
                    totalCost += selectedCard.cost;
                }
            }
        }
    }

    private bool Roll()
    {
        int roll = UnityEngine.Random.Range(0, 2);
        if (roll == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
