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
