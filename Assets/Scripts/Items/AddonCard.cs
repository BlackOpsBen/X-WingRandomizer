using System;
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
    public bool reinforce;

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
    public bool hasIllicitEquipped;

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
    public bool grantsCrew2;
    public bool grantsIllicit;
    public bool grantsModificationCosting3OrLess;
    public bool grantsBomb;
    public bool grantsBomb2;
    public bool grantsModification;
    public bool grantsModification2;
    public bool grantsTorpedo;
    public bool grantsSystem;
    public bool grantsCannon;
    public bool grantsCannon2;
    public bool grantsMissile;
    public bool grantsSalvagedAstromech;
    public bool grantsCannonTorpedoOrMissile;

    [Header("Misc Properties")]
    public bool Discard;

    private void Awake()
    {
        InitializeAddon();
    }
    private void OnValidate()
    {
        InitializeAddon();
    }

    private void InitializeAddon()
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
            reinforce,
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
            hasTurretEquipped,
            hasIllicitEquipped
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
        if (grantsCannonTorpedoOrMissile)
        {
            DetermineWhichToGrant();
        }

        slotGrantings = new bool[]
        {
            grantsElitePilotTalent,
            grantsCrew,
            grantsCrew2,
            grantsIllicit,
            grantsModificationCosting3OrLess,
            grantsBomb,
            grantsBomb2,
            grantsModification,
            grantsModification2,
            grantsTorpedo,
            grantsSystem,
            grantsCannon,
            grantsCannon2,
            grantsMissile,
            grantsSalvagedAstromech,
            grantsCannonTorpedoOrMissile
        };
    }

    private void DetermineWhichToGrant()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                grantsCannon = true;
                grantsCannonTorpedoOrMissile = false;
                break;
            case 1:
                grantsTorpedo = true;
                grantsCannonTorpedoOrMissile = false;
                break;
            case 2:
                grantsMissile = true;
                grantsCannonTorpedoOrMissile = false;
                break;
            default:
                Debug.LogError("Invalid random number!");
                break;
        }
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

    public int[] GetSlotGrantings()
    {
        int[] numTypes = new int[AddonCardManager.Instance.GetNumAddonTypes() + 2]; // +2 is to account for "Modification costing 3 or less" as different from "Modification" and "Titles"
        for (int i = 0; i < numTypes.Length; i++)
        {
            numTypes[i] = 0;
        }
        if (grantsElitePilotTalent)
        {
            numTypes[0]++;
        }
        if (grantsCrew)
        {
            numTypes[8]++;
        }
        if (grantsCrew2)
        {
            numTypes[8]++;
        }
        if (grantsIllicit)
        {
            numTypes[11]++;
        }
        if (grantsModificationCosting3OrLess)
        {
            numTypes[13]++;
        }
        if (grantsBomb)
        {
            numTypes[3]++;
        }
        if (grantsBomb2)
        {
            numTypes[3]++;
        }
        if (grantsModification)
        {
            numTypes[4]++;
        }
        if (grantsModification2)
        {
            numTypes[4]++;
        }
        if (grantsTorpedo)
        {
            numTypes[1]++;
        }
        if (grantsSystem)
        {
            numTypes[9]++;
        }
        if (grantsCannon)
        {
            numTypes[6]++;
        }
        if (grantsCannon2)
        {
            numTypes[6]++;
        }
        if (grantsMissile)
        {
            numTypes[2]++;
        }
        if (grantsSalvagedAstromech)
        {
            numTypes[12]++;
        }
        if (grantsCannonTorpedoOrMissile)
        {
            Debug.LogError("This should never be reached!");
        }

        return numTypes;
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

    public int GetThisTypeIndex()
    {
        string[] itemTypes = AddonCardManager.Instance.addonCardNames;

        for (int i = 0; i < itemTypes.Length; i++)
        {
            if (this.GetType().ToString() == itemTypes[i])
            {
                return i;
            }
        }

        return 14; // this is for when it's a "Title" card which doesn't exist in the type list.
    }
}
