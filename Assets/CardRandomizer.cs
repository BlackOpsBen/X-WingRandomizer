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
                    } while (ValidateSelection(selectedCard) == false);
                    
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

    private bool ValidateSelection(AddonCard addonCard)
    {
        if (addonCard.GetHasRestrictions())
        {
            if (addonCard.unique)
            {
                // if a previous card is the same card, return false
                // if a current neighboring card is the same card, return false
            }

            if (addonCard.limited)
            {
                // if a current neighboring card is the same card, return false
            }

            // Faction restrictions
            if (addonCard.rebelOnly && !ship.rebel)
            {
                return false;
            }

            if (addonCard.imperialOnly && !ship.imperial)
            {
                return false;
            }

            if (addonCard.scumOnly && !ship.scum)
            {
                return false;
            }

            if (addonCard.rebelAndScumOnly && (!ship.rebel || !ship.scum))
            {
                return false;
            }

            // Ship size restrictions
            if (addonCard.smallShipOnly && !ship.smallShip)
            {
                return false;
            }

            if (addonCard.largeShipOnly && !ship.largeShip)
            {
                return false;
            }

            if (addonCard.hugeShipOnly && !ship.hugeShip)
            {
                return false;
            }

            // Ship type restrictions
            if (addonCard.tiePhantomOnly && ship.name != "TIE Phantom")
            {
                return false;
            }

            if (addonCard.bWingOnly && ship.name != "B-Wing")
            {
                return false;
            }

            if (addonCard.GR75Only && ship.name != "GR-75")
            {
                return false;
            }

            if (addonCard.BSF17BomberOnly && ship.name != "B/SF-17 Bomber")
            {
                return false;
            }

            if (addonCard.lancerClassPursuitCraftOnly && ship.name != "Lancer-Class Pursuit Craft")
            {
                return false;
            }

            if (addonCard.xWingOnly && ship.name != "X-Wing" && ship.name != "T-70 X-Wing")
            {
                return false;
            }

            if (addonCard.YV666Only && ship.name != "YV-666")
            {
                return false;
            }

            if (addonCard.YT1300AndYT2400Only && ship.name != "YT-1300" && ship.name != "YT-2400")
            {
                return false;
            }

            if (addonCard.TIEFighterOnly && ship.name != "TIE Fighter" && ship.name != "Rebel TIE Fighter")
            {
                return false;
            }

            if (addonCard.quadjumperOnly && ship.name != "Quadjumper")
            {
                return false;
            }

            if (addonCard.heavyTIEOnly && !ship.heavyTIE)
            {
                return false;
            }

            if (addonCard.TIEOnly && !ship.TIE)
            {
                return false;
            }

            if (addonCard.T65xWingOnly && ship.name != "X-Wing")
            {
                return false;
            }

            if (addonCard.aWingOnly && ship.name != "A-Wing")
            {
                return false;
            }

            if (addonCard.yWingOnly && ship.name != "Y-Wing")
            {
                return false;
            }

            // Action Restrictions
            if (addonCard.focus && !ship.focus)
            {
                return false;
            }

            if (addonCard.targetLock && !ship.targetLock)
            {
                return false;
            }

            if (addonCard.boost && !ship.boost)
            {
                return false;
            }

            if (addonCard.evade && !ship.evade)
            {
                return false;
            }

            if (addonCard.barrelRoll && !ship.barrelRoll)
            {
                return false;
            }

            if (addonCard.cloak && !ship.cloak)
            {
                return false;
            }

            if (addonCard.boostOrBarrelRoll && !ship.boost && !ship.barrelRoll)
            {
                return false;
            }

            if (addonCard.focusOrEvade && !ship.focus && !ship.evade)
            {
                return false;
            }

            // Misc Restrictions
            if (addonCard.cantAlreadyHaveElitePilotTalentSlot && pilot.GetAddonTypeQuantity(0) > 0)
            {
                return false;
            }

            if (addonCard.hasMinPilotSkill)
            {
                if (pilot.GetPilotSkill() < addonCard.minPilotSkill)
                {
                    return false;
                }
            }

            if (addonCard.hasMaxPilotSkill)
            {
                if (pilot.GetPilotSkill() > addonCard.maxPilotSkill)
                {
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileEquipped && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                return false;
            }

            if (addonCard.torpedoOrMissileOrBombEquipped && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
            {
                return false;
            }

            if (addonCard.hasTorpedoOrMissileSlot && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                return false;
            }

            if (addonCard.requiresShieldValue1 && ship.shieldValue != 1)
            {
                return false;
            }
        }

        // Check for redundant ability
        if (addonCard.grantsFocus && ship.focus)
        {
            return false;
        }
        else
        {
            bool previousCardDoes = false;
            foreach (AddonCard prevCard in addonCards)
            {
                if (prevCard.grantsFocus)
                {
                    previousCardDoes = true;
                }
            }
            if (previousCardDoes)
            {
                return false;
            }
        }

        if (addonCard.grantsTargetLock && ship.targetLock)
        {
            return false;
        }
        else
        {
            bool previousCardDoes = false;
            foreach (AddonCard prevCard in addonCards)
            {
                if (prevCard.grantsTargetLock)
                {
                    previousCardDoes = true;
                }
            }
            if (previousCardDoes)
            {
                return false;
            }
        }

        if (addonCard.grantsBoost && ship.boost)
        {
            return false;
        }
        else
        {
            bool previousCardDoes = false;
            foreach (AddonCard prevCard in addonCards)
            {
                if (prevCard.grantsBoost)
                {
                    previousCardDoes = true;
                }
            }
            if (previousCardDoes)
            {
                return false;
            }
        }

        if (addonCard.grantsEvade && ship.evade)
        {
            return false;
        }
        else
        {
            bool previousCardDoes = false;
            foreach (AddonCard prevCard in addonCards)
            {
                if (prevCard.grantsEvade)
                {
                    previousCardDoes = true;
                }
            }
            if (previousCardDoes)
            {
                return false;
            }
        }

        if (addonCard.barrelRoll && ship.barrelRoll)
        {
            return false;
        }
        else
        {
            bool previousCardDoes = false;
            foreach (AddonCard prevCard in addonCards)
            {
                if (prevCard.grantsBarrelRoll)
                {
                    previousCardDoes = true;
                }
            }
            if (previousCardDoes)
            {
                return false;
            }
        }

        return true;
    }
}
