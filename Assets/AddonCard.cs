using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class AddonCard : ScriptableObject
{
    public Sprite cardArt;

    public int cost;

    public RestrictionListGroup[] restrictionListGroups;

    private ListManager[] ListManagers;

    public RestrictionListGroup[] backup;

    private void OnValidate()
    {
        backup = restrictionListGroups;

        // Get all Restriction Lists
        ListManagers = Resources.LoadAll<ListManager>("Restriction Lists");

        // Set array to same number of list groups
        restrictionListGroups = new RestrictionListGroup[ListManagers.Length];

        for (int i = 0; i < restrictionListGroups.Length; i++)
        {
            restrictionListGroups[i] = new RestrictionListGroup();
            restrictionListGroups[i].restrictionLists = new RestrictionList[ListManagers[i].objectLists.Length];
            restrictionListGroups[i].name = ListManagers[i].name;

            for (int j = 0; j < restrictionListGroups[i].restrictionLists.Length; j++)
            {
                restrictionListGroups[i].restrictionLists[j] = new RestrictionList();
                restrictionListGroups[i].restrictionLists[j].restrictions = new Restriction[ListManagers[i].objectLists[j].objects.Length];
                restrictionListGroups[i].restrictionLists[j].name = ListManagers[i].objectLists[j].name;

                for (int k = 0; k < restrictionListGroups[i].restrictionLists[j].restrictions.Length; k++)
                {
                    restrictionListGroups[i].restrictionLists[j].restrictions[k] = new Restriction();
                    restrictionListGroups[i].restrictionLists[j].restrictions[k].name = ListManagers[i].objectLists[j].objects[k].name;

                    if (i < backup.Length && j < backup[i].restrictionLists.Length && k < backup[i].restrictionLists[j].restrictions.Length)
                    {
                        restrictionListGroups[i].restrictionLists[j].restrictions[k].restriction = backup[i].restrictionLists[j].restrictions[k].restriction;
                    }
                }
            }
        }

        

        
    }

    /*DELETE FROM HERE...
    private bool hasRestrictions;
    private bool[] restrictions;

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
    public bool SLAM;

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

    [Header("Grants actions/abilities:")]
    public bool grantsFocus;
    public bool grantsTargetLock;
    public bool grantsBoost;
    public bool grantsEvade;
    public bool grantsBarrelRoll;
    ... TO HERE*/

    /* LIST OF RESTRICTIONS
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
            SLAM,
            cantAlreadyHaveElitePilotTalentSlot,
            hasMinPilotSkill,
            hasMaxPilotSkill,
            torpedoOrMissileEquipped,
            torpedoOrMissileOrBombEquipped,
            hasTorpedoOrMissileSlot,
            requiresShieldValue1
        };
    } */
}
[System.Serializable]
public class RestrictionListGroup
{
    [HideInInspector] public string name;

    public RestrictionList[] restrictionLists;
}


[System.Serializable]
public class RestrictionList
{
    [HideInInspector] public string name;

    public Restriction[] restrictions;
}

[System.Serializable]
public class Restriction
{
    [HideInInspector] public string name;

    public bool restriction;
}