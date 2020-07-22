using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AddonCard : Item, IComeInProducts
{
    public bool forcePushChanges;

    public Sprite cardArt;

    public int cost;

    [SerializeField] private List<Product> includedWith = new List<Product>();

    private bool hasRestrictions;
    private bool[] restrictions;

    private bool[] actionGrantings;
    private bool[] slotGrantings;

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
    public bool TIEAdvancedOnly;

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
    public bool torpedoOrBombEquipped;
    public bool hasTorpedoOrMissileSlot;
    public bool requiresShieldValue1;
    public bool bombEquipped;
    public bool hasAstromechEquipped;
    public bool hasAttackTargetLockEquipped;
    public bool hasShields;
    public bool hasTurretEquipped;

    [Header("Grants actions/abilities:")]
    public bool grantsFocus;
    public bool grantsTargetLock;
    public bool grantsBoost;
    public bool grantsEvade;
    public bool grantsBarrelRoll;
    public bool grantsActionHeader;
    public bool grantsAttackTargetLock;
    public bool grantsSLAM;
    public bool grantsCloak;

    [Header("Grants new slots to fill")]
    public bool grantsElitePilotTalent;
    public bool grantsCrew;
    public bool grantsIllicit;
    public bool grantsModificationCosting3OrLess;
    public bool grantsBomb;
    public bool grantsBomb2;
    public bool grantsModification;
    public bool grantsTorpedo;

    private void OnValidate()
    {
        MakeListOfRestrictions();
        MakeListOfActionGrantings();
        MakeListOfSlotGrantings();
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
            TIEAdvancedOnly,
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
            torpedoOrBombEquipped,
            hasTorpedoOrMissileSlot,
            requiresShieldValue1,
            bombEquipped,
            hasAstromechEquipped,
            hasAttackTargetLockEquipped,
            hasShields,
            hasTurretEquipped
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
            grantsActionHeader,
            grantsAttackTargetLock,
            grantsSLAM,
            grantsCloak
        };
    }

    private void MakeListOfSlotGrantings()
    {
        slotGrantings = new bool[]
        {
            grantsElitePilotTalent,
            grantsCrew,
            grantsIllicit,
            grantsModificationCosting3OrLess,
            grantsBomb,
            grantsBomb2,
            grantsModification,
            grantsTorpedo
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

    public bool GetGrantsSlot()
    {
        for (int i = 0; i < slotGrantings.Length; i++)
        {
            if (slotGrantings[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool GetActionGranting(int aGranting)
    {
        return actionGrantings[aGranting];
    }

    public bool GetRestriction(int restriction)
    {
        return restrictions[restriction];
    }

    public List<Product> GetProductsIncludedWith()
    {
        return includedWith;
    }

    public string GetName()
    {
        return this.name;
    }

    public void ReciprocateInclusion(Product product)
    {
        includedWith.Add(product);
    }

    public int GetProductListCount()
    {
        return includedWith.Count;
    }
}
