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
            if (Squadrons.Instance.GetPreviousShipHas("IG-2000"))
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
            return false;
        }
        #endregion

        #region Maul
        if (addonCard.GetName() == "Maul")
        {
            // First check if another faction already has Maul
            if (Squadrons.Instance.GetUniqueAlreadyTakenAnyFaction(addonCard.GetName()))
            {
                Debug.LogWarning("Maul is already taken by another faction.");
                return false;
            }

            // Then make sure a rebel can claim him.
            if (ship.rebel)
            {
                string ezraBridger = "Ezra Bridger";
                if (!Squadrons.Instance.GetPreviousShipHas(ezraBridger) && !cardRandomizer.PreviousCardIs(ezraBridger) && pilot.GetName() != ezraBridger && !Squadrons.Instance.GetPreviousPilotIs(ezraBridger))
                {
                    return false;
                }
            }
        }
        #endregion

        #region Tail Gunner
        if (addonCard.GetName() == "Tail Gunner")
        {
            string[] validShips = new string[]
            {
                "Firespray-31",
                "ARC-170",
                "Sheathipede-Class Shuttle",
                "TIEsf",
                "Firespray-31 (Scum)"
            };

            bool isValidShip = false;

            for (int i = 0; i < validShips.Length; i++)
            {
                if (validShips[i] == ship.GetName())
                {
                    isValidShip = true;
                }
            }
            if (!isValidShip)
            {
                return false;
            }
        }
        #endregion

        #region Ketsu Onyo
        if (addonCard.GetName() == "Ketsu Onyo")
        {
            if (!Squadrons.Instance.GetPreviousShipHas("Tractor Beam") && !Squadrons.Instance.GetPreviousShipHas("Spacetug Tractor Array"))
            {
                return false;
            }
            
        }
        #endregion

        #region Kullbee Sperado
        if (pilot.GetName() == "Kullbee Sperado" && addonCard.GetType().ToString() == "Modification" && addonCard.GetName() != "Servomotor S-Foils")
        {
            return false;
        }
        #endregion

        #region Attanni Mindlink
        if (addonCard.GetType().ToString() == "ElitePilotTalent" && cardRandomizer.mustPickAttanniMindlink && addonCard.GetName() != "Attanni Mindlink")
        {
            return false;
        }

        if (addonCard.GetName() == "Attanni Mindlink")
        {
            if (cardRandomizer.cantPickAttanniMindlink)
            {
                return false;
            }
            if (Squadrons.Instance.GetPointsRemaining() - cardRandomizer.totalCost < 37)
            {
                return false;
            }
        }
        #endregion

        #region Tomax Bren
        if (pilot.GetName() == "Tomax Bren" && addonCard.GetType().ToString() == "ElitePilotTalent" && addonCard.Discard == false)
        {
            return false;
        }
        #endregion

        #region Youngster
        if (pilot.GetName() == "Youngster" && addonCard.GetType().ToString() == "ElitePilotTalent" && addonCard.grantsActionHeader == false)
        {
            return false;
        }
        #endregion

        return true;
    }
}