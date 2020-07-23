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
        if ((addonCard.torpedoOrMissileEquipped || addonCard.torpedoOrMissileOrBombEquipped) && cardRandomizer.PreviousCardIs("Chardaan Refit"))
        {
            return false;
        }

        if (addonCard.name == "Chardaan Refit" && (cardRandomizer.PreviousCardRequires(37) || cardRandomizer.PreviousCardRequires(38)))
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

        return true;
    }
}

/*
 * Chopper Astromech requires another upgrade card of any kind
 * 
 * Death Troopers require 2 crew slots, and fill both.
 * 
 * Emporer Palpatine requires 2 crew slots, and fill both
 * 
 * ISB Slicer crew requires jam action
 * 
 * Maul crew is scum only unless you already have "Ezra Bridger" crew in squad
 * 
 * Tail Gunner crew only valid for ships with rear aux arc. (Firespray and ARC-130 and Sheathipede-class Shuttle)
 * 
 * IG-88D crew requires another ship with IG-2000
 * 
 * Ketsu Onyo crew requires tractor beam ability
 * 
 * Cikatro Vizago crew requires an Illicit equipped
 * 
 * Jabba crew requires 2 crew slots, AND requires at least 1 Illicit equipped
 * 
 * Breach specialist crew requires Reinforce action
 * 
 * Wookie Commandos crew requires 2 crew slots
 * 
 * 
