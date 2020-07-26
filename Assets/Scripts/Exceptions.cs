using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exceptions : MonoBehaviour
{
    private CardRandomizer cardRandomizer;

    private void Awake()
    {
        cardRandomizer = GetComponent<CardRandomizer>();
    }

    public bool ValidateExceptions(AddonCard addonCard, PilotCard pilot, Ship ship)
    {
        #region Chardaan Refit
        if ((addonCard.torpedoOrMissileEquipped || addonCard.torpedoOrMissileOrBombEquipped || addonCard.torpedoOrBombEquipped) && cardRandomizer.PreviousCardIs("Chardaan Refit"))
        {
            return false;
        }

        if (addonCard.name == "Chardaan Refit" && (cardRandomizer.PreviousCardRequires(39) || cardRandomizer.PreviousCardRequires(40) || cardRandomizer.PreviousCardRequires(41)))
        {
            return false;
        }
        #endregion

        #region Extra Munitions
        if (addonCard.name == "Extra Munitions" && pilot.GetAddonTypeQuantity(1) < 2 && pilot.GetAddonTypeQuantity(2) == 0 && pilot.GetAddonTypeQuantity(3) == 0)
        {
            return false;
        }
        #endregion

        #region Collision Detector
        if (addonCard.name == "Collision Detector" && !ship.boost && !ship.barrelRoll && !ship.cloak && !cardRandomizer.PreviousCardGrantsAction(2) && !cardRandomizer.PreviousCardGrantsAction(4) && !cardRandomizer.PreviousCardGrantsAction(8))
        {
            return false;
        }
        #endregion

        #region Trajectory Simulator
        if (addonCard.name == "Trajectory Simulator")
        {
            bool hasNonActionBombEquipped = false;
            foreach (AddonCard addon in cardRandomizer.addonCards)
            {
                if (addon.GetType().ToString() == "Bomb" && !addon.grantsActionHeader)
                {
                    hasNonActionBombEquipped = true;
                }
            }
            return hasNonActionBombEquipped;
        }
        #endregion

        #region Thrust Corrector
        if (addonCard.name == "Thrust Corrector" && ship.hullValue < 5)
        {
            return false;
        }
        #endregion

        #region Saw's Renegades
        if (addonCard.GetName() == "Saw's Renegades")
        {
            if (ship.GetName() == "X-Wing" || ship.GetName() == "U-Wing")
            {
                // Valid. Do nothing.
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IG-88D
        if (addonCard.GetName() == "IG-88D")
        {
            if (Squadrons.Instance.GetPreviousShipHas("IG-2000", PilotCardManager.Instance.GetSelectedFactionIndex()))
            {
                // Valid. Do nothing.
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ISB Slicer
        if (addonCard.GetName() == "ISB Slicer" && ship.GetName() != "TIE Reaper")
        {
            Debug.Log(addonCard.GetName() + " requires the ability to do the Jam action. Only the TIE Reaper can.");
            return false;
        }
        #endregion

        #region Maul
        if (addonCard.GetName() == "Maul")
        {
            if (ship.rebel)
            {
                string ezraBridger = "Ezra Bridger";
                int factionIndex = PilotCardManager.Instance.GetSelectedFactionIndex();
                if (!Squadrons.Instance.GetPreviousShipHas(ezraBridger, factionIndex) && !cardRandomizer.PreviousCardIs(ezraBridger) && pilot.GetName() != ezraBridger && Squadrons.Instance.GetPreviousPilotIs(ezraBridger, factionIndex))
                {
                    Debug.Log(addonCard.GetName() + " is only valid for rebels IF Ezra Bridger is selected.");
                    return false;
                }
            }
        }
        #endregion

        return true;
    }
}

/*
 * Maul crew is scum only unless you already have "Ezra Bridger" crew in squad
 * 
 * Tail Gunner crew only valid for ships with rear aux arc. (Firespray and ARC-130 and Sheathipede-class Shuttle and TIE/sf Fighter)
 * 
 * Ketsu Onyo crew requires tractor beam ability (SHadow caster pilot Ketsu does but he's the same person!)
 * 
 * Cikatro Vizago crew requires an Illicit equipped
 * 
 * Tactical Officer crew grants Coordinate. (Do any cards require coordinate?) Also Sheathipede has coordinate
 * 
 * Attani Mindlink elite pilot talent requires 2 ships to have it. If last possible ship, don't allow. Otherwise, require that the next ship chosen can and will also take it.
 * 
 * Advanced Ailerons title for TIE Reaper only
 */
 