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
            GenerateNewSet();
        }
    }

    private void GenerateNewSet()
    {
        addonCards.Clear();
        totalCost = 0;
        MakeRandomPilot();
        SelectAddons();

        GetComponent<DisplayCards>().DisplayAddons(addonCards);
    }

    private void MakeRandomPilot()
    {
        // Randomly selects Ship
        int randShip = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelShips.Length);
        ship = PilotCardManager.Instance.rebelShips[randShip];

        // Randomly selects Pilot based on selected Ship
        int randPilot = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelPilotGroups[randShip].pilots.Length);
        pilot = PilotCardManager.Instance.rebelPilotGroups[randShip].pilots[randPilot];

        // Creates array of addon card slots
        pilot.MakeList();

        // Displays the pilot on the card model
        GetComponent<DisplayCards>().DisplayPilot(pilot.GetTexture());

        // Adds the cost of the selected Pilot
        totalCost += pilot.GetCost();
    }

    private void SelectAddons()
    {
        for (int i = 0; i < pilot.GetNumAddonTypes(); i++)
        {
            for (int j = 0; j < pilot.GetAddonTypeQuantity(i); j++)
            {
                // roll to see if to be filled
                if (true)
                {
                    int randMax = AddonCardManager.Instance.GetAddonCardGroupLength(i);
                    int rand;

                    AddonCard selectedCard;

                    do
                    {
                        rand = UnityEngine.Random.Range(0, randMax);
                        selectedCard = AddonCardManager.Instance.GetAddonCard(i, rand);
                    } while (false);
                    
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
