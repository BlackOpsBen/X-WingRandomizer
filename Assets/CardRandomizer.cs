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

    private DisplayCards displayCards;

    private void Awake()
    {
        displayCards = GetComponent<DisplayCards>();
    }

    // Called by UI Button
    public void GenerateNewSet()
    {
        ResetPilot();
        MakeRandomPilot();
        SelectAddons();

        FillAnyNewSlots();

        GetComponent<DisplayCards>().DisplayAddons(addonCards);

        UIManager.Instance.EnableKeepOrPass();
    }

    private void ResetPilot()
    {
        addonCards.Clear();
        totalCost = 0;
    }

    private void MakeRandomPilot()
    {
        int randShip = RollForShip(out ship);

        pilot = RollForPilot(randShip);

        pilot.MakeList();

        displayCards.DisplayPilot(pilot.GetTexture());

        totalCost += pilot.GetCost();
    }

    private int RollForShip(out Ship s)
    {
        int result = UnityEngine.Random.Range(0, PilotCardManager.Instance.factionList[PilotCardManager.Instance.selectedFaction].ships.Length);
        s = PilotCardManager.Instance.factionList[PilotCardManager.Instance.selectedFaction].ships[result];
        return result;
    }

    private PilotCard RollForPilot(int randShip)
    {
        int randPilot = UnityEngine.Random.Range(0, PilotCardManager.Instance.factionList[PilotCardManager.Instance.selectedFaction].pilotGroups[randShip].pilots.Length);
        return PilotCardManager.Instance.factionList[PilotCardManager.Instance.selectedFaction].pilotGroups[randShip].pilots[randPilot];
    }

    private void SelectAddons()
    {
        for (int i = 0; i < pilot.GetNumAddonTypes(); i++)
        {
            for (int j = 0; j < pilot.GetAddonTypeQuantity(i); j++)
            {
                if (true) // TODO set this back to Roll() and make better odds
                {
                    MakeValidSelection(i);
                }
            }
        }
    }

    private void MakeValidSelection(int addonType)
    {
        AddonCard selection;
        if (selection = RandomlySelectAddon(addonType))
        {
            addonCards.Add(selection);
        }
    }

    private AddonCard RandomlySelectAddon(int addonType)
    {
        List<AddonCard> allCards = new List<AddonCard>();

        for (int k = 0; k < AddonCardManager.Instance.GetAddonCardGroupLength(addonType); k++)
        {
            if (ValidateSelection(AddonCardManager.Instance.GetAddonCard(addonType, k)))
            {
                allCards.Add(AddonCardManager.Instance.GetAddonCard(addonType, k));
            }
        }

        if (allCards.Count > 0)
        {
            int randMax = allCards.Count;
            int rand;

            AddonCard selectedCard;

            rand = UnityEngine.Random.Range(0, randMax);
            selectedCard = allCards[rand];

            totalCost += selectedCard.cost; // TODO delegate cost counting elsewhere

            Debug.Log("Selected " + selectedCard.name);

            return selectedCard;
        }
        else
        {
            Debug.LogWarning("Attempted to select a card but there were no valid choices.");
            return null;
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

    private void FillAnyNewSlots()
    {
        foreach (AddonCard addonCard in addonCards)
        {
            if (addonCard.grantsCrew)
            {
                MakeValidSelection(8);
                Debug.Log("Verify this selection was of type 'Crew'");
            }

            if (addonCard.grantsElitePilotTalent)
            {
                MakeValidSelection(0);
                Debug.Log("Verify this selection was of type 'Elite Pilot Talent'");
            }

            if (addonCard.grantsIllicit)
            {
                MakeValidSelection(11);
                Debug.Log("Verify this selection was of type 'Illicit'");
            }

            if (addonCard.grantsModificationCosting3OrLess)
            {
                AddonCard newMod;

                do
                {
                    newMod = RandomlySelectAddon(4);
                } while (newMod.cost > 3);

                // TODO need to make valid selection if no valid card exists. Avoid inf loop
                
                Debug.Log("Verify this selection was of type 'Modification' and that it costs 3 or less points.");
            }
        }
    }

    private bool PreviousCardValidates(int aGrantingIndex)
    {
        foreach (AddonCard addonCard in addonCards)
        {
            if (addonCard.GetActionGranting(aGrantingIndex))
            {
                return true;
            }
        }
        return false;
    }

    private bool ValidateSelection(AddonCard addonCard)
    {
        if (addonCard.GetHasRestrictions())
        {
            if (addonCard.unique)
            {
                //Debug.Log(addonCard.name + " is Unique. Checking to see if it was already purchased in this squadron...");
                // if a previous card is the same card, return false
                // if a current neighboring card is the same card, return false
            }

            if (addonCard.limited)
            {
                //Debug.Log(addonCard.name + " is Limited. Checking to see if it was already purchased for this ship.");
                // if a current neighboring card is the same card, return false
            }

            // Faction restrictions
            if (addonCard.rebelOnly && !ship.rebel)
            {
                //Debug.Log(addonCard.name + " requires Rebel Ship. Invalid selection.");
                return false;
            }

            if (addonCard.imperialOnly && !ship.imperial)
            {
                //Debug.Log(addonCard.name + " requires Imperial Ship. Invalid selection.");
                return false;
            }

            if (addonCard.scumOnly && !ship.scum)
            {
                //Debug.Log(addonCard.name + " requires Scum Ship. Invalid selection.");
                return false;
            }

            if (addonCard.rebelAndScumOnly && (!ship.rebel || !ship.scum))
            {
                //Debug.Log(addonCard.name + " requires Rebel or Scum Ship. Invalid selection.");
                return false;
            }

            // Ship size restrictions
            if (addonCard.smallShipOnly && !ship.smallShip)
            {
                //Debug.Log(addonCard.name + " requires Small Ship. Invalid selection.");
                return false;
            }

            if (addonCard.largeShipOnly && !ship.largeShip)
            {
                //Debug.Log(addonCard.name + " requires Large Ship. Invalid selection.");
                return false;
            }

            if (addonCard.hugeShipOnly && !ship.hugeShip)
            {
                //Debug.Log(addonCard.name + " requires Huge Ship. Invalid selection.");
                return false;
            }

            // Ship type restrictions
            if (addonCard.tiePhantomOnly && ship.name != "TIE Phantom")
            {
                //Debug.Log(addonCard.name + " requires TIE Phantom. Invalid selection.");
                return false;
            }

            if (addonCard.bWingOnly && ship.name != "B-Wing")
            {
                //Debug.Log(addonCard.name + " requires B-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.GR75Only && ship.name != "GR-75")
            {
                //Debug.Log(addonCard.name + " requires GR-75 Ship. Invalid selection.");
                return false;
            }

            if (addonCard.BSF17BomberOnly && ship.name != "B/SF-17 Bomber")
            {
                //Debug.Log(addonCard.name + " requires B/SF-17 Bomber. Invalid selection.");
                return false;
            }

            if (addonCard.lancerClassPursuitCraftOnly && ship.name != "Lancer-Class Pursuit Craft")
            {
                //Debug.Log(addonCard.name + " requires Lancer-Class Pursuit Craft. Invalid selection.");
                return false;
            }

            if (addonCard.xWingOnly && ship.name != "X-Wing" && ship.name != "T-70 X-Wing")
            {
                //Debug.Log(addonCard.name + " requires any X-Wing type. Invalid selection.");
                return false;
            }

            if (addonCard.YV666Only && ship.name != "YV-666")
            {
                //Debug.Log(addonCard.name + " requires YV-666. Invalid selection.");
                return false;
            }

            if (addonCard.YT1300AndYT2400Only && ship.name != "YT-1300" && ship.name != "YT-2400")
            {
                //Debug.Log(addonCard.name + " requires YT-1300. Invalid selection.");
                return false;
            }

            if (addonCard.TIEFighterOnly && ship.name != "TIE Fighter" && ship.name != "Rebel TIE Fighter")
            {
                //Debug.Log(addonCard.name + " requires TIE Fighter. Invalid selection.");
                return false;
            }

            if (addonCard.quadjumperOnly && ship.name != "Quadjumper")
            {
                //Debug.Log(addonCard.name + " requires Quadjumper. Invalid selection.");
                return false;
            }

            if (addonCard.heavyTIEOnly && !ship.heavyTIE)
            {
                //Debug.Log(addonCard.name + " requires any heavy TIE type. Invalid selection.");
                return false;
            }

            if (addonCard.TIEOnly && !ship.TIE)
            {
                //Debug.Log(addonCard.name + " requires any TIE type. Invalid selection.");
                return false;
            }

            if (addonCard.T65xWingOnly && ship.name != "X-Wing")
            {
                //Debug.Log(addonCard.name + " requires X-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.aWingOnly && ship.name != "A-Wing")
            {
                //Debug.Log(addonCard.name + " requires A-Wing. Invalid selection.");
                return false;
            }

            if (addonCard.yWingOnly && ship.name != "Y-Wing")
            {
                //Debug.Log(addonCard.name + " requires Y-Wing. Invalid selection.");
                return false;
            }

            // Action Restrictions
            if (addonCard.focus && !ship.focus && PreviousCardValidates(0))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Focus. Invalid selection.");
                return false;
            }

            if (addonCard.targetLock && !ship.targetLock && PreviousCardValidates(1))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Target Lock. Invalid selection.");
                return false;
            }

            if (addonCard.boost && !ship.boost && PreviousCardValidates(2))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Boost. Invalid selection.");
                return false;
            }

            if (addonCard.evade && !ship.evade && PreviousCardValidates(3))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Evade. Invalid selection.");
                return false;
            }

            if (addonCard.barrelRoll && !ship.barrelRoll && PreviousCardValidates(4))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Barrel Roll. Invalid selection.");
                return false;
            }

            if (addonCard.cloak && !ship.cloak)
            {
                //Debug.Log(addonCard.name + " requires a ship that can Cloak. Invalid selection.");
                return false;
            }

            if (addonCard.boostOrBarrelRoll && !ship.boost && !ship.barrelRoll && PreviousCardValidates(2) && PreviousCardValidates(4))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Boost or Barrel Roll. Invalid selection.");
                return false;
            }

            if (addonCard.focusOrEvade && !ship.focus && !ship.evade && PreviousCardValidates(0) && PreviousCardValidates(3))
            {
                //Debug.Log(addonCard.name + " requires a ship that can Focus or Evade. Invalid selection.");
                return false;
            }

            if (addonCard.slam && !ship.slam)
            {
                //Debug.Log(addonCard.name + " requires a ship that can SLAM. Invalid selection.");
                return false;
            }

            // Misc Restrictions
            if (addonCard.cantAlreadyHaveElitePilotTalentSlot && pilot.GetAddonTypeQuantity(0) > 0)
            {
                //Debug.Log(addonCard.name + " requires a pilot that does not already have an Elite Pilot Talent slot. Invalid selection.");
                return false;
            }

            if (addonCard.hasMinPilotSkill)
            {
                if (pilot.GetPilotSkill() < addonCard.minPilotSkill)
                {
                    //Debug.Log(addonCard.name + " requires a pilot skill of at least " + addonCard.minPilotSkill + ". Invalid selection.");
                    return false;
                }
            }

            if (addonCard.hasMaxPilotSkill)
            {
                if (pilot.GetPilotSkill() > addonCard.maxPilotSkill)
                {
                    //Debug.Log(addonCard.name + " requires a pilot skill of at most " + addonCard.maxPilotSkill + ". Invalid selection.");
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileEquipped)
            {
                if (pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
                {
                    Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a torpedo or missile.");
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileOrBombEquipped)
            {
                if (pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
                {
                    Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a torpedo or missile or bomb.");
                    return false;
                }
            }

            if (addonCard.hasTorpedoOrMissileSlot && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                //Debug.Log(addonCard.name + " requires a Torpedo or Missile slot. Invalid selection.");
                return false;
            }

            if (addonCard.requiresShieldValue1 && ship.shieldValue != 1)
            {
                //Debug.Log(addonCard.name + " requires a Shield Value of exactly 1. Invalid selection.");
                return false;
            }

            if (addonCard.bombEquipped && pilot.GetAddonTypeQuantity(3) == 0)
            {
                Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a bomb.");
                return false;
            }

            if (addonCard.hasAstromechEquipped && pilot.GetAddonTypeQuantity(5) == 0)
            {
                Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip an Astromech.");
                return false;
            }
        }
        else
        {
            //Debug.Log(addonCard.name + " does NOT have any restrictions.");
        }

        // Check for redundant ability
        if (addonCard.grantsFocus && ship.focus)
        {
            //Debug.Log(addonCard.name + " grants a redundant ability (Focus). Invalid selection.");
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
                //Debug.Log(addonCard.name + " grants Focus, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsTargetLock && ship.targetLock)
        {
            //Debug.Log(addonCard.name + " grants a redundant ability (Target Lock). Invalid selection.");
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
                //Debug.Log(addonCard.name + " grants Target Lock, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsBoost && ship.boost)
        {
            //Debug.Log(addonCard.name + " grants a redundant ability (Boost). Invalid selection.");
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
                //Debug.Log(addonCard.name + " grants Boost, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsEvade && ship.evade)
        {
            //Debug.Log(addonCard.name + " grants a redundant ability (Evade). Invalid selection.");
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
                //Debug.Log(addonCard.name + " grants Evade, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        if (addonCard.grantsBarrelRoll && ship.barrelRoll)
        {
            //Debug.Log(addonCard.name + " grants a redundant ability (Barrel Roll). Invalid selection.");
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
                //Debug.Log(addonCard.name + " grants Barrel Roll, but a previously selected card already grants it. Invalid selection.");
                return false;
            }
        }

        //Debug.Log("All requirements met for equipping " + addonCard.name + ". This card is valid!");
        return true;
    }
}
