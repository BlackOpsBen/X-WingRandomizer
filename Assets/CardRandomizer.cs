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
            Debug.Log(addonCard.name + " has restrictions.");

            if (addonCard.unique)
            {
                Debug.Log(addonCard.name + " is Unique. Checking to see if it was already purchased in this squadron...");
                // if a previous card is the same card, return false
                // if a current neighboring card is the same card, return false
            }

            if (addonCard.limited)
            {
                Debug.Log(addonCard.name + " is Limited. Checking to see if it was already purchased for this ship.");
                // if a current neighboring card is the same card, return false
            }

            // Faction restrictions
            if (addonCard.rebelOnly && !ship.rebel)
            {
                Debug.Log(addonCard.name + " requires Rebel Ship. Invalid selection.");
                return false;
            }

            if (addonCard.imperialOnly && !ship.imperial)
            {
                Debug.Log(addonCard.name + " requires Imperial Ship. Invalid selection.");
                return false;
            }

            if (addonCard.scumOnly && !ship.scum)
            {
                Debug.Log(addonCard.name + " requires Scum Ship. Invalid selection.");
                return false;
            }

            if (addonCard.rebelAndScumOnly && (!ship.rebel || !ship.scum))
            {
                Debug.Log(addonCard.name + " requires Rebel or Scum Ship. Invalid selection.");
                return false;
            }

            // Ship size restrictions
            if (addonCard.smallShipOnly && !ship.smallShip)
            {
                Debug.Log(addonCard.name + " requires Small Ship. Invalid selection.");
                return false;
            }

            if (addonCard.largeShipOnly && !ship.largeShip)
            {
                Debug.Log(addonCard.name + " requires Large Ship. Invalid selection.");
                return false;
            }

            if (addonCard.hugeShipOnly && !ship.hugeShip)
            {
                Debug.Log(addonCard.name + " requires Huge Ship. Invalid selection.");
                return false;
            }

            // Ship type restrictions
            if (addonCard.tiePhantomOnly && ship.name != "TIE Phantom")
            {
                Debug.Log(addonCard.name + " requires TIE Phantom. Invalid selection.");
                return false;
            }

            if (addonCard.bWingOnly && ship.name != "B-Wing")
            {
                Debug.Log(addonCard.name + " requires B-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.GR75Only && ship.name != "GR-75")
            {
                Debug.Log(addonCard.name + " requires GR-75 Ship. Invalid selection.");
                return false;
            }

            if (addonCard.BSF17BomberOnly && ship.name != "B/SF-17 Bomber")
            {
                Debug.Log(addonCard.name + " requires B/SF-17 Bomber. Invalid selection.");
                return false;
            }

            if (addonCard.lancerClassPursuitCraftOnly && ship.name != "Lancer-Class Pursuit Craft")
            {
                Debug.Log(addonCard.name + " requires Lancer-Class Pursuit Craft. Invalid selection.");
                return false;
            }

            if (addonCard.xWingOnly && ship.name != "X-Wing" && ship.name != "T-70 X-Wing")
            {
                Debug.Log(addonCard.name + " requires any X-Wing type. Invalid selection.");
                return false;
            }

            if (addonCard.YV666Only && ship.name != "YV-666")
            {
                Debug.Log(addonCard.name + " requires YV-666. Invalid selection.");
                return false;
            }

            if (addonCard.YT1300AndYT2400Only && ship.name != "YT-1300" && ship.name != "YT-2400")
            {
                Debug.Log(addonCard.name + " requires YT-1300. Invalid selection.");
                return false;
            }

            if (addonCard.TIEFighterOnly && ship.name != "TIE Fighter" && ship.name != "Rebel TIE Fighter")
            {
                Debug.Log(addonCard.name + " requires TIE Fighter. Invalid selection.");
                return false;
            }

            if (addonCard.quadjumperOnly && ship.name != "Quadjumper")
            {
                Debug.Log(addonCard.name + " requires Quadjumper. Invalid selection.");
                return false;
            }

            if (addonCard.heavyTIEOnly && !ship.heavyTIE)
            {
                Debug.Log(addonCard.name + " requires any heavy TIE type. Invalid selection.");
                return false;
            }

            if (addonCard.TIEOnly && !ship.TIE)
            {
                Debug.Log(addonCard.name + " requires any TIE type. Invalid selection.");
                return false;
            }

            if (addonCard.T65xWingOnly && ship.name != "X-Wing")
            {
                Debug.Log(addonCard.name + " requires X-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.aWingOnly && ship.name != "A-Wing")
            {
                Debug.Log(addonCard.name + " requires A-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.yWingOnly && ship.name != "Y-Wing")
            {
                Debug.Log(addonCard.name + " requires Y-Wing. Invalid selection.");
                return false;
            }

            // Action Restrictions
            if (addonCard.focus && !ship.focus)
            {
                Debug.Log(addonCard.name + " requires a ship that can Focus. Invalid selection.");
                return false;
            }

            if (addonCard.targetLock && !ship.targetLock)
            {
                Debug.Log(addonCard.name + " requires a ship that can Target Lock. Invalid selection.");
                return false;
            }

            if (addonCard.boost && !ship.boost)
            {
                Debug.Log(addonCard.name + " requires a ship that can Boost. Invalid selection.");
                return false;
            }

            if (addonCard.evade && !ship.evade)
            {
                Debug.Log(addonCard.name + " requires a ship that can Evade. Invalid selection.");
                return false;
            }

            if (addonCard.barrelRoll && !ship.barrelRoll)
            {
                Debug.Log(addonCard.name + " requires a ship that can Barrel Roll. Invalid selection.");
                return false;
            }

            if (addonCard.cloak && !ship.cloak)
            {
                Debug.Log(addonCard.name + " requires a ship that can Cloak. Invalid selection.");
                return false;
            }

            if (addonCard.boostOrBarrelRoll && !ship.boost && !ship.barrelRoll)
            {
                Debug.Log(addonCard.name + " requires a ship that can Boost or Barrel Roll. Invalid selection.");
                return false;
            }

            if (addonCard.focusOrEvade && !ship.focus && !ship.evade)
            {
                Debug.Log(addonCard.name + " requires a ship that can Focus or Evade. Invalid selection.");
                return false;
            }

            // Misc Restrictions
            if (addonCard.cantAlreadyHaveElitePilotTalentSlot && pilot.GetAddonTypeQuantity(0) > 0)
            {
                Debug.Log(addonCard.name + " requires a pilot that does not already have an Elite Pilot Talent slot. Invalid selection.");
                return false;
            }

            if (addonCard.hasMinPilotSkill)
            {
                if (pilot.GetPilotSkill() < addonCard.minPilotSkill)
                {
                    Debug.Log(addonCard.name + " requires a pilot skill of at least " + addonCard.minPilotSkill + ". Invalid selection.");
                    return false;
                }
            }

            if (addonCard.hasMaxPilotSkill)
            {
                if (pilot.GetPilotSkill() > addonCard.maxPilotSkill)
                {
                    Debug.Log(addonCard.name + " requires a pilot skill of at most " + addonCard.maxPilotSkill + ". Invalid selection.");
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileEquipped && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                Debug.Log(addonCard.name + " requires a Torpedo or Missile to be equipped. Invalid selection.");
                Debug.LogWarning("Need to allow for potential selection of Torpedo or Missile to make " + addonCard.name + " a valid selection.");
                return false;
            }

            if (addonCard.torpedoOrMissileOrBombEquipped && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
            {
                Debug.Log(addonCard.name + " requires a Torpedo or Missile or Bomb to be equipped. Invalid selection.");
                Debug.LogWarning("Need to allow for potential selection of Torpedo or Missile or Bomb to make " + addonCard.name + " a valid selection.");
                return false;
            }

            if (addonCard.hasTorpedoOrMissileSlot && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                Debug.Log(addonCard.name + " requires a Torpedo or Missile slot. Invalid selection.");
                return false;
            }

            if (addonCard.requiresShieldValue1 && ship.shieldValue != 1)
            {
                Debug.Log(addonCard.name + " requires a Shield Value of exactly 1. Invalid selection.");
                return false;
            }
        }
        else
        {
            Debug.Log(addonCard.name + " does NOT have any restrictions.");
        }

        // Check for redundant ability
        if (addonCard.grantsFocus && ship.focus)
        {
            Debug.Log(addonCard.name + " grants a redundant ability (Focus). Invalid selection.");
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
                Debug.Log(addonCard.name + " grants Focus, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsTargetLock && ship.targetLock)
        {
            Debug.Log(addonCard.name + " grants a redundant ability (Target Lock). Invalid selection.");
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
                Debug.Log(addonCard.name + " grants Target Lock, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsBoost && ship.boost)
        {
            Debug.Log(addonCard.name + " grants a redundant ability (Boost). Invalid selection.");
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
                Debug.Log(addonCard.name + " grants Boost, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsEvade && ship.evade)
        {
            Debug.Log(addonCard.name + " grants a redundant ability (Evade). Invalid selection.");
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
                Debug.Log(addonCard.name + " grants Evade, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.barrelRoll && ship.barrelRoll)
        {
            Debug.Log(addonCard.name + " grants a redundant ability (Barrel Roll). Invalid selection.");
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
                Debug.Log(addonCard.name + " grants Barrel Roll, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }
        Debug.Log("All requirements met for equipping " + addonCard.name + ". This card is valid!");
        return true;
    }
}
