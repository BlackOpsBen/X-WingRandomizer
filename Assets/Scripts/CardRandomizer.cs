using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomizer : MonoBehaviour
{
    [SerializeField] private bool debugReasons = false;

    [HideInInspector]
    public Ship ship;
    [HideInInspector]
    public PilotCard pilot;
    public List<AddonCard> addonCards;
    public int totalCost;

    private DisplayCards displayCards;

    private Exceptions exceptions;

    private bool needToFillNewSlots = false;
    private int lastCountedCardIndex = 0;

    // Title specific modifications
    public bool nextSystemIsMinus4 = false; // TIEx1
    public bool allUpgradesAreMinus1 = false; // Vaksai
    private bool onlyUniqueSalvagedAstromechs = false; // Havoc
    private bool crewsCost4OrLess = false; // TIE Shuttle

    private bool losesCannon = false;
    private bool losesMissile = false;
    private bool losesCrew = false;
    private bool losesAllOrdnance = false;

    // Bizarrely specific exception helpers
    private bool has2OrMoreSlots = false;
    private bool tookCardFilling2Slots = false;
    public bool allElitesAreMinus1 = false; // Renegade Refit
    private bool modMustCost3OrLess = false; // Smuggling

    private int costModifiers = 0;

    public bool mustPickAttanniMindlink = false;
    public bool cantPickAttanniMindlink = false;

    private void Awake()
    {
        displayCards = GetComponent<DisplayCards>();
        exceptions = GetComponent<Exceptions>();
    }

    // Called by UI Button
    public void GenerateNewSet()
    {
        ResetPilot();
        MakeRandomPilot();
        SelectTitle();
        SelectAddons();

        while (needToFillNewSlots)
        {
            FillAnyNewSlots();
        }
        lastCountedCardIndex = 0;

        SetCostModifiers();

        CalculateTotalCost();

        GetComponent<DisplayCards>().DisplayAddons(addonCards);

        UIManager.Instance.EnableKeepOrPass();
    }

    public int GetCostModifiers()
    {
        return costModifiers;
    }

    public void SetCostModifiers()
    {
        costModifiers = 0;

        if (nextSystemIsMinus4)
        {
            int mostExpensiveSystem = 0;
            foreach (AddonCard addonCard in addonCards)
            {
                if (addonCard.GetType().Name == "SystemUpgrade")
                {
                    if (addonCard.cost > mostExpensiveSystem)
                    {
                        mostExpensiveSystem = addonCard.cost;
                    }
                }
            }
            
            costModifiers += Mathf.Min(4, mostExpensiveSystem);
        }

        if (allUpgradesAreMinus1)
        {
            int totalDiscount = 0;
            foreach (AddonCard addonCard in addonCards)
            {
                if (addonCard.cost > 0)
                {
                    totalDiscount++;
                }
            }
            
            costModifiers += totalDiscount;
        }

        if (allElitesAreMinus1)
        {
            int totalDiscount = 0;
            foreach (AddonCard addonCard in addonCards)
            {
                if (addonCard.GetType().Name == "ElitePilotTalent" && addonCard.cost > 0)
                {
                    totalDiscount++;
                }
            }
            
            costModifiers += totalDiscount;
        }
    }

    public void CalculateTotalCost()
    {
        int subtotalCost = 0;
        if (pilot)
        {
            subtotalCost += pilot.GetCost();
        }
        
        foreach (AddonCard addonCard in addonCards)
        {
            subtotalCost += addonCard.cost;
        }
        subtotalCost -= costModifiers;
        totalCost = subtotalCost;

        UIManager.Instance.DisplaySetCost(totalCost);
    }

    private void ResetPilot()
    {
        addonCards.Clear();
        totalCost = 0;

        nextSystemIsMinus4 = false;
        allUpgradesAreMinus1 = false;
        onlyUniqueSalvagedAstromechs = false;
        crewsCost4OrLess = false;

        losesCannon = false;
        losesMissile = false;
        losesCrew = false;
        losesAllOrdnance = false;

        has2OrMoreSlots = false;
        tookCardFilling2Slots = false;
        allElitesAreMinus1 = false;

        costModifiers = 0;
    }

    private void MakeRandomPilot()
    {
        int randShip = RollForShip(out ship);

        pilot = RollForPilot(randShip);

        pilot.MakeList();

        displayCards.DisplayPilot(pilot.GetTexture());
    }

    private int RollForShip(out Ship selectedShip)
    {
        List<Ship> affordableShips = PilotCardManager.Instance.GetAffordableShips(Squadrons.Instance.GetPointsRemaining());

        int result = UnityEngine.Random.Range(0, affordableShips.Count);

        selectedShip = affordableShips[result];

        for (int i = 0; i < PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].ships.Length; i++)
        {
            string possibleShipName = PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].ships[i].name;
            if (possibleShipName == selectedShip.name)
            {
                return i;
            }
        }
        Debug.LogError("Failed to map affordable ship index to ALL SHIPS index.");
        return result;
    }

    private PilotCard RollForPilot(int randShip)
    {
        List<PilotCard> affordablePilots = PilotCardManager.Instance.GetAffordablePilots(Squadrons.Instance.GetPointsRemaining(), randShip);
        PilotCard potentialPilot;

        int infLoopLimiter = 0;

        do
        {
            int randPilot = UnityEngine.Random.Range(0, affordablePilots.Count);
            potentialPilot = affordablePilots[randPilot];

            infLoopLimiter++;

        } while (UniquePilotAlreadyTaken(potentialPilot) && infLoopLimiter < 100);

        if (infLoopLimiter == 100)
        {
            Debug.LogError("Unable to find a pilot that isn't an already taken \"unique\" pilot for " + ship.name + ".");
        }

        return potentialPilot;
    }

    private bool UniquePilotAlreadyTaken(PilotCard pilotCard)
    {
        if (pilotCard.GetIsUnique())
        {
            return Squadrons.Instance.GetUniqueAlreadyTaken(pilotCard.name);
        }
        else
        {
            return false;
        }
    }

    private void SelectTitle()
    {
        Title[] titleOptions = ship.GetTitleOptions();
        List<Title> validOptions = new List<Title>();

        if (titleOptions.Length > 0)
        {
            for (int i = 0; i < titleOptions.Length; i++)
            {
                if (ValidateSelection(titleOptions[i]))
                {
                    if (titleOptions[i].noBrainer)
                    {
                        validOptions.Add(titleOptions[i]);
                    }
                    else
                    {
                        int roll = UnityEngine.Random.Range(0, 2);
                        if (roll == 1)
                        {
                            validOptions.Add(titleOptions[i]);
                        }
                    }
                }
            }
        }
        if (validOptions.Count > 0)
        {
            Title selectedTitle;

            if (pilot.GetName() == "Quinn Jast")
            {
                selectedTitle = validOptions[0];
            }
            else
            {
                int rand = UnityEngine.Random.Range(0, validOptions.Count);

                selectedTitle = validOptions[rand];
            }

            addonCards.Add(selectedTitle);

            if (selectedTitle.GetGrantsSlot())
            {
                needToFillNewSlots = true;
            }

            if (selectedTitle.GetName() == "TIEx1")
            {
                nextSystemIsMinus4 = true;
            }
            else if (selectedTitle.GetName() == "Vaksai")
            {
                allUpgradesAreMinus1 = true;
            }
            else if (selectedTitle.GetName() == "StarViper MkII")
            {
                int coin = UnityEngine.Random.Range(0, 2);
                if (coin == 1)
                {
                    addonCards.Add(titleOptions[0]);
                    if (titleOptions[0].GetGrantsSlot())
                    {
                        needToFillNewSlots = true;
                    }
                }
            }
            else if (selectedTitle.GetName() == "Havoc")
            {
                onlyUniqueSalvagedAstromechs = true;
            }
            else if (selectedTitle.GetName() == "TIE Shuttle")
            {
                crewsCost4OrLess = true;
            }
            else if (selectedTitle.GetName() == "Mist Hunter")
            {
                AddonCard tractorBeamCard;
                for (int i = 0; i < AddonCardManager.Instance.GetAddonCardGroupLength(6); i++)
                {
                    if (AddonCardManager.Instance.GetAddonCard(6, i).GetName() == "Tractor Beam")
                    {
                        tractorBeamCard = AddonCardManager.Instance.GetAddonCard(6, i);
                        addonCards.Add(tractorBeamCard);
                    }
                }
            }

            losesCannon = selectedTitle.losesCannon;
            losesMissile = selectedTitle.losesMissile;
            losesCrew = selectedTitle.losesCrew;
            losesAllOrdnance = selectedTitle.losesAllOrdnance;
}
    }

    private void SelectAddons()
    {
        int numToLoop = pilot.GetNumAddonTypes();
        List<int> orderedList = new List<int>();
        for (int i = 0; i < numToLoop; i++)
        {
            orderedList.Add(i);
        }

        List<int> unorderedList = new List<int>();
        for (int i = 0; i < numToLoop; i++)
        {
            int index = UnityEngine.Random.Range(0, orderedList.Count);
            int rand = orderedList[index];
            unorderedList.Add(rand);
            orderedList.RemoveAt(index);
        }
        
        for (int i = 0; i < pilot.GetNumAddonTypes(); i++)
        {
            int quantityToFill = pilot.GetAddonTypeQuantity(unorderedList[i]);

            if (unorderedList[i] == 6 && losesCannon)
            {
                quantityToFill--;
            }
            else if (unorderedList[i] == 8 && losesCrew)
            {
                quantityToFill--;
            }
            else if (unorderedList[i] == 2 && losesMissile)
            {
                quantityToFill--;
            }
            else if (losesAllOrdnance && ( (unorderedList[i] == 1) || (unorderedList[i] == 2) || (unorderedList[i] == 3) ) )
            {
                quantityToFill = 0;
            }

            for (int j = 0; j < quantityToFill; j++)
            {
                tookCardFilling2Slots = false;

                if (quantityToFill - j > 1)
                {
                    has2OrMoreSlots = true;
                }
                else
                {
                    has2OrMoreSlots = false;
                }

                MakeValidSelection(unorderedList[i]);

                if (tookCardFilling2Slots)
                {
                    j++;
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
            
            if (selection.GetGrantsSlot())
            {
                needToFillNewSlots = true;
            }

            // 2 Slot Exception
            if (selection.GetName() == "Death Troopers" || selection.GetName() == "Wookie Commandos" || selection.GetName() == "Emperor Palpatine" || selection.GetName() == "Jabba the Hutt" || selection.GetName() == "Bomblet Generator")
            {
                tookCardFilling2Slots = true;
            }

            // Renegade Refit exception
            if (selection.GetName() == "Renegade Refit")
            {
                allElitesAreMinus1 = true;
            }
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

            return selectedCard;
        }
        else
        {
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
        needToFillNewSlots = false;
        int currentCardCount = addonCards.Count;

        for (int i = lastCountedCardIndex; i < currentCardCount; i++)
        {
            if (addonCards[i].grantsCrew)
            {
                MakeValidSelection(8);
            }

            if (addonCards[i].grantsCrew2)
            {
                MakeValidSelection(8);
            }

            if (addonCards[i].grantsElitePilotTalent)
            {
                MakeValidSelection(0);
            }

            if (addonCards[i].grantsIllicit)
            {
                MakeValidSelection(11);
            }

            if (addonCards[i].grantsModificationCosting3OrLess)
            {
                modMustCost3OrLess = true;

                AddonCard newMod = RandomlySelectAddon(4);

                modMustCost3OrLess = false;

                addonCards.Add(newMod);

                if (newMod.GetGrantsSlot())
                {
                    needToFillNewSlots = true;
                }
            }

            if (addonCards[i].grantsBomb)
            {
                MakeValidSelection(3);
            }

            if (addonCards[i].grantsBomb2)
            {
                MakeValidSelection(3);
            }

            if (addonCards[i].grantsModification)
            {
                MakeValidSelection(4);
            }

            if (addonCards[i].grantsModification2)
            {
                MakeValidSelection(4);
            }

            if (addonCards[i].grantsTorpedo)
            {
                MakeValidSelection(1);
            }

            if (addonCards[i].grantsSystem)
            {
                MakeValidSelection(9);
            }

            if (addonCards[i].grantsCannon)
            {
                MakeValidSelection(6);
            }

            if (addonCards[i].grantsCannon2)
            {
                MakeValidSelection(6);
            }

            if (addonCards[i].grantsMissile)
            {
                MakeValidSelection(2);
            }

            if (addonCards[i].grantsSalvagedAstromech)
            {
                MakeValidSelection(12);
            }

            if (addonCards[i].grantsCannonTorpedoOrMissile)
            {
                Debug.LogWarning("This should no longer be called!");
                int[] options = new int[] { 6, 1, 2 };
                List<int> typeOptions = new List<int>(options);
                int[] unorderedOptions = new int[3];

                for (int r = 0; r < options.Length; r++)
                {
                    int rand = UnityEngine.Random.Range(0, typeOptions.Count);
                    unorderedOptions[r] = typeOptions[rand];
                    typeOptions.RemoveAt(rand);
                }

                for (int t = 0; t < unorderedOptions.Length; t++)
                {
                    bool optionExists = false;

                    for (int p = 0; p < AddonCardManager.Instance.GetAddonCardGroupLength(unorderedOptions[t]); p++)
                    {
                        if (ValidateSelection(AddonCardManager.Instance.GetAddonCard(unorderedOptions[t], p)))
                        {
                            optionExists = true;
                            p = 1000;
                        }
                    }

                    if (optionExists)
                    {
                        MakeValidSelection(unorderedOptions[t]);
                        t = 1000;
                    }
                }
            }

            lastCountedCardIndex++;
        }
    }

    public bool TypeIsSelected(int type)
    {
        foreach (AddonCard addonCard in addonCards)
        {
            if (addonCard.GetThisTypeIndex() == type)
            {
                return true;
            }
        }
        return false;
    }

    public bool PreviousCardGrantsAction(int aGrantingIndex)
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

    public bool PreviousCardIs(string name)
    {
        foreach (AddonCard addonCard in addonCards)
        {
            if (addonCard.name == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool PreviousCardRequires(int restriction)
    {
        foreach (AddonCard addonCard in addonCards)
        {
            if (addonCard.GetRestriction(restriction))
            {
                return true;
            }
        }
        return false;
    }

    public int PreviousCardsCost()
    {
        int subtotalCost = 0;
        foreach (AddonCard addonCard in addonCards)
        {
            subtotalCost += addonCard.cost;
        }
        return subtotalCost;
    }

    private bool ValidateSelection(AddonCard addonCard)
    {
        bool isIncluded = DisplayProductToggles.Instance.GetIsEnabled(addonCard);
        if (!isIncluded)
        {
            if (debugReasons) { Debug.Log(addonCard.name + " is not available with the selected products."); }
            return false;
        }

        int pointLimit = Squadrons.Instance.GetPointsRemaining() - PreviousCardsCost() - pilot.GetCost();

        int netCardCost = addonCard.cost;

        // For Mist Hunter Title and Tractor Beam requirement
        if (addonCard.GetName() == "Mist Hunter")
        {
            netCardCost++;
        }

        if (netCardCost > pointLimit)
        {
            if (debugReasons) { Debug.Log(addonCard.name + " too expensive."); }
            return false;
        }

        if (addonCard.GetHasRestrictions())
        {
            if (addonCard.unique)
            {
                if (Squadrons.Instance.GetUniqueAlreadyTaken(addonCard.name) || PreviousCardIs(addonCard.name) || pilot.GetName() == addonCard.name)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " unique and already taken."); }
                    return false;
                }
            }

            if (addonCard.limited)
            {
                if (PreviousCardIs(addonCard.name))
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " is limited and already on this ship."); }
                    return false;
                }
            }

            // Faction restrictions
            if (addonCard.rebelOnly && !ship.rebel)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Rebel Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.imperialOnly && !ship.imperial)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Imperial Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.scumOnly && !ship.scum)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Scum Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.rebelAndScumOnly && !ship.rebel && !ship.scum)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Rebel or Scum Ship. Invalid selection."); }
                return false;
            }

            // Ship size restrictions
            if (addonCard.smallShipOnly && !ship.smallShip)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Small Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.largeShipOnly && !ship.largeShip)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Large Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.hugeShipOnly && !ship.hugeShip)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Huge Ship. Invalid selection."); }
                return false;
            }

            // Ship type restrictions
            if (addonCard.tiePhantomOnly && ship.name != "TIE Phantom")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires TIE Phantom. Invalid selection."); }
                return false;
            }

            if (addonCard.bWingOnly && ship.name != "B-Wing")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires B-Wing. Invalid selection."); }
                return false;
            }

            if (addonCard.GR75Only && ship.name != "GR-75")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires GR-75 Ship. Invalid selection."); }
                return false;
            }

            if (addonCard.BSF17BomberOnly && ship.name != "B/SF-17 Bomber")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires B/SF-17 Bomber. Invalid selection."); }
                return false;
            }

            if (addonCard.lancerClassPursuitCraftOnly && ship.name != "Lancer-Class Pursuit Craft")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Lancer-Class Pursuit Craft. Invalid selection."); }
                return false;
            }

            if (addonCard.xWingOnly && ship.name != "X-Wing" && ship.name != "T-70 X-Wing")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires any X-Wing type. Invalid selection."); }
                return false;
            }

            if (addonCard.YV666Only && ship.name != "YV-666")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires YV-666. Invalid selection."); }
                return false;
            }

            if (addonCard.YT1300AndYT2400Only && ship.name != "YT-1300" && ship.name != "YT-2400")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires YT-1300. Invalid selection."); }
                return false;
            }

            if (addonCard.TIEFighterOnly && ship.name != "TIE Fighter" && ship.name != "Rebel TIE Fighter")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires TIE Fighter. Invalid selection."); }
                return false;
            }

            if (addonCard.quadjumperOnly && ship.name != "Quadjumper")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Quadjumper. Invalid selection."); }
                return false;
            }

            if (addonCard.heavyTIEOnly && !ship.heavyTIE)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires any heavy TIE type. Invalid selection."); }
                return false;
            }

            if (addonCard.TIEOnly && !ship.TIE)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires any TIE type. Invalid selection."); }
                return false;
            }

            if (addonCard.T65xWingOnly && ship.name != "X-Wing")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires X-Wing. Invalid selection."); }
                return false;
            }

            if (addonCard.aWingOnly && ship.name != "A-Wing")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires A-Wing. Invalid selection."); }
                return false;
            }

            if (addonCard.yWingOnly && ship.name != "Y-Wing")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires Y-Wing. Invalid selection."); }
                return false;
            }

            if (addonCard.TIEAdvancedOnly && ship.name != "TIE Advanced")
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires TIE Advanced. Invalid selection."); }
                return false;
            }

            // Action Restrictions
            if (addonCard.focus && !ship.focus && !PreviousCardGrantsAction(0))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Focus. Invalid selection."); }
                return false;
            }

            if (addonCard.targetLock && !ship.targetLock && !PreviousCardGrantsAction(1))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Target Lock. Invalid selection."); }
                return false;
            }

            if (addonCard.boost && !ship.boost && !PreviousCardGrantsAction(2))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Boost. Invalid selection."); }
                return false;
            }

            if (addonCard.evade && !ship.evade && !PreviousCardGrantsAction(3))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Evade. Invalid selection."); }
                return false;
            }

            if (addonCard.barrelRoll && !ship.barrelRoll && !PreviousCardGrantsAction(4))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Barrel Roll. Invalid selection."); }
                return false;
            }

            if (addonCard.cloak && !ship.cloak && !PreviousCardGrantsAction(8))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Cloak. Invalid selection."); }
                return false;
            }

            if (addonCard.boostOrBarrelRoll && !ship.boost && !ship.barrelRoll && !PreviousCardGrantsAction(2) && !PreviousCardGrantsAction(4))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Boost or Barrel Roll. Invalid selection."); }
                return false;
            }

            if (addonCard.focusOrEvade && !ship.focus && !ship.evade && !PreviousCardGrantsAction(0) && !PreviousCardGrantsAction(3))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can Focus or Evade. Invalid selection."); }
                return false;
            }

            if (addonCard.slam && !ship.slam && !PreviousCardGrantsAction(7))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that can SLAM. Invalid selection."); }
                return false;
            }

            if (addonCard.actionHeader && !PreviousCardGrantsAction(5))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a card that has an \"Action\" header. Invalid selection."); }
                return false;
            }

            if (addonCard.reinforce && !ship.reinforce)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a ship that has the \"Reinforce\" action. Invalid selection."); }
                return false;
            }

            // Misc Restrictions
            if (addonCard.cantAlreadyHaveElitePilotTalentSlot && pilot.GetAddonTypeQuantity(0) > 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a pilot that does not already have an Elite Pilot Talent slot. Invalid selection."); }
                return false;
            }

            if (addonCard.hasMinPilotSkill)
            {
                if (pilot.GetPilotSkill() < addonCard.minPilotSkill)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " requires a pilot skill of at least " + addonCard.minPilotSkill + ". Invalid selection."); }
                    return false;
                }
            }

            if (addonCard.hasMaxPilotSkill)
            {
                if (pilot.GetPilotSkill() > addonCard.maxPilotSkill)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " requires a pilot skill of at most " + addonCard.maxPilotSkill + ". Invalid selection."); }
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileEquipped)
            {
                if (pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a torpedo or missile."); }
                    return false;
                }
            }

            if (addonCard.torpedoOrMissileOrBombEquipped)
            {
                if (pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a torpedo or missile or bomb."); }
                    return false;
                }
            }

            if (addonCard.torpedoOrBombEquipped)
            {
                if (pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a torpedo or bomb."); }
                    return false;
                }
            }

            if (addonCard.hasTorpedoOrMissileSlot && pilot.GetAddonTypeQuantity(1) == 0 && pilot.GetAddonTypeQuantity(2) == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a Torpedo or Missile slot. Invalid selection."); }
                return false;
            }

            if (addonCard.requiresShieldValue1 && ship.shieldValue != 1)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires a Shield Value of exactly 1. Invalid selection."); }
                return false;
            }

            if (addonCard.bombEquipped && pilot.GetAddonTypeQuantity(3) == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip a bomb."); }
                return false;
            }

            if (addonCard.hasAstromechEquipped && pilot.GetAddonTypeQuantity(5) == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because " + pilot.name + " can't even equip an Astromech."); }
                return false;
            }

            if (addonCard.hasAttackTargetLockEquipped && !PreviousCardGrantsAction(6))
            {
                if (debugReasons) { Debug.Log(addonCard.name + " won't be equipped because no addon card has Attack Target Lock."); }
                return false;
            }

            if (addonCard.hasShields && ship.shieldValue == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because this ship has no shields."); }
                return false;
            }

            if (addonCard.hasTurretEquipped && pilot.GetAddonTypeQuantity(7) == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because it requires a Turret."); }
                return false;
            }

            if (addonCard.hasIllicitEquipped && pilot.GetAddonTypeQuantity(11) == 0)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " can't be equipped because it requires an Illicit."); }
                return false;
            }
        }

        // Exceptions

        if (exceptions.ValidateExceptions(addonCard, pilot, ship) == false)
        {
            if (debugReasons) { Debug.Log(addonCard.name + " has exceptions that aren't valid."); }
            return false;
        }

        if (addonCard.GetType().Name == "SalvagedAstromech" && onlyUniqueSalvagedAstromechs)
        {
            if (!addonCard.unique)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " Havoc title requires unique Salvaged Astromechs only."); }
                return false;
            }
        }

        if (addonCard.GetType().Name == "Crew" && crewsCost4OrLess)
        {
            if (addonCard.cost > 4)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " is too expensive. TIE Shuttle requires cost of 4 or less."); }
                return false;
            }
        }

        if (addonCard.GetName() == "Death Troopers")
        {
            if (!has2OrMoreSlots)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " requires 2 Crew slots but only 1 is available."); }
                return false;
            }
        }

        if (modMustCost3OrLess && addonCard.cost > 3)
        {
            return false;
        }


        // Check for redundant ability
        if (addonCard.grantsFocus)
        {
            if (ship.focus)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (Focus). Invalid selection."); }
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
                    if (debugReasons) { Debug.Log(addonCard.name + " grants Focus, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (addonCard.grantsTargetLock)
        {
            if (ship.targetLock)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (Target Lock). Invalid selection."); }
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
                    if (debugReasons) { Debug.Log(addonCard.name + " grants Target Lock, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (addonCard.grantsBoost)
        {
            if (ship.boost)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (Boost). Invalid selection."); }
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
                    if (debugReasons) { Debug.Log(addonCard.name + " grants Boost, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (addonCard.grantsEvade)
        {
            if (ship.evade)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (Evade). Invalid selection."); }
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
                    if (debugReasons) { Debug.Log(addonCard.name + " grants Evade, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (addonCard.grantsBarrelRoll)
        {
            if (ship.barrelRoll)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (Barrel Roll). Invalid selection."); }
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
                    if (debugReasons) { Debug.Log(addonCard.name + " grants Barrel Roll, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (addonCard.grantsSLAM)
        {
            if (ship.slam)
            {
                if (debugReasons) { Debug.Log(addonCard.name + " grants a redundant ability (SLAM). Invalid selection."); }
                return false;
            }
            else
            {
                bool previousCardDoes = false;
                foreach (AddonCard prevCard in addonCards)
                {
                    if (prevCard.grantsSLAM)
                    {
                        previousCardDoes = true;
                    }
                }
                if (previousCardDoes)
                {
                    if (debugReasons) { Debug.Log(addonCard.name + " grants SLAM, but a previously selected card already grants it. Invalid selection."); }
                    return false;
                }
            }
        }

        if (debugReasons) { Debug.Log("All requirements met for equipping " + addonCard.name + ". This card is valid!"); }
        return true;
    }
}
