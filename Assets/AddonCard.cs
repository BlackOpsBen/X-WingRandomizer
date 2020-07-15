using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AddonCard : ScriptableObject
{
    public bool forcePushChanges;

    public Sprite cardArt;

    public int cost;

    private bool hasRestrictions;
    private bool[] restrictions;

    private bool[] actionGrantings;

    [Header("Restrictions")]
    public bool unique;
    public bool limited;

    [Header("Faction Restrictions")]
    public bool rebelOnly;
    public bool imperialOnly;
    public bool scumOnly;
    public bool rebelAndScumOnly;

    [Header("Class Restrictions")]
    public bool smallShipOnly;
    public bool largeShipOnly;
    public bool hugeShipOnly;

    [Header("Ship Restrictions")]
    public bool tiePhantomOnly;
    public bool bWingOnly;
    public bool GR75Only;
    public bool BSF17BomberOnly;
    public bool lancerClassPursuitCraftOnly;
    public bool xWingOnly;
    public bool YV666Only;
    public bool YT1300AndYT2400Only;
    public bool TIEFighterOnly;
    public bool quadjumperOnly;
    public bool heavyTIEOnly;
    public bool TIEOnly;
    public bool T65xWingOnly;
    public bool aWingOnly;
    public bool yWingOnly;

    [Header("Action Requirements")]
    public bool focus;
    public bool targetLock;
    public bool boost;
    public bool evade;
    public bool barrelRoll;
    public bool cloak;
    public bool boostOrBarrelRoll;
    public bool focusOrEvade;
    public bool slam;
    public bool actionHeader;

    [Header("Misc Restrictions")]
    public bool cantAlreadyHaveElitePilotTalentSlot;
    public bool hasMinPilotSkill;
    public int minPilotSkill;
    public bool hasMaxPilotSkill;
    public int maxPilotSkill;
    public bool torpedoOrMissileEquipped;
    public bool torpedoOrMissileOrBombEquipped;
    public bool hasTorpedoOrMissileSlot;
    public bool requiresShieldValue1;
    public bool bombEquipped;
    public bool hasAstromechEquipped;

    [Header("Grants actions/abilities:")]
    public bool grantsFocus;
    public bool grantsTargetLock;
    public bool grantsBoost;
    public bool grantsEvade;
    public bool grantsBarrelRoll;
    public bool grantsActionHeader;

    [Header("Grants new slots to fill")]
    public bool grantsElitePilotTalent;
    public bool grantsCrew;
    public bool grantsIllicit;
    public bool grantsModificationCosting3OrLess;
    public bool grantsBomb;

    private void OnValidate()
    {
        MakeListOfRestrictions();
        MakeListOfActionGrantings();
        hasRestrictions = SetHasRestrictions();
        forcePushChanges = false;
    }

    private void MakeListOfRestrictions()
    {
        restrictions = new bool[]
        {
            unique,
            limited,
            rebelOnly,
            imperialOnly,
            scumOnly,
            rebelAndScumOnly,
            smallShipOnly,
            largeShipOnly,
            hugeShipOnly,
            tiePhantomOnly,
            bWingOnly,
            GR75Only,
            BSF17BomberOnly,
            lancerClassPursuitCraftOnly,
            xWingOnly,
            YV666Only,
            YT1300AndYT2400Only,
            TIEFighterOnly,
            quadjumperOnly,
            heavyTIEOnly,
            TIEOnly,
            T65xWingOnly,
            aWingOnly,
            yWingOnly,
            focus,
            targetLock,
            boost,
            evade,
            barrelRoll,
            cloak,
            boostOrBarrelRoll,
            focusOrEvade,
            slam,
            actionHeader,
            cantAlreadyHaveElitePilotTalentSlot,
            hasMinPilotSkill,
            hasMaxPilotSkill,
            torpedoOrMissileEquipped,
            torpedoOrMissileOrBombEquipped,
            hasTorpedoOrMissileSlot,
            requiresShieldValue1,
            bombEquipped,
            hasAstromechEquipped
        };
    }

    private void MakeListOfActionGrantings()
    {
        actionGrantings = new bool[]
        {
            grantsFocus,
            grantsTargetLock,
            grantsBoost,
            grantsEvade,
            grantsBarrelRoll,
            grantsActionHeader
        };
    }

    private bool SetHasRestrictions()
    {
        for (int i = 0; i < restrictions.Length; i++)
        {
            if (restrictions[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool GetHasRestrictions()
    {
        return hasRestrictions;
    }

    public bool GetActionGranting(int aGranting)
    {
        return actionGrantings[aGranting];
    }

    public bool GetRestriction(int restriction)
    {
        return restrictions[restriction];
    }
}
