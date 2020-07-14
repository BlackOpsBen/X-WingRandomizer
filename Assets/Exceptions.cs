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

        return true;
    }
}
