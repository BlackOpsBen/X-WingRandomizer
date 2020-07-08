using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class AddonCard : ScriptableObject
{
    public Sprite cardArt;

    public int cost;

    //public ShipList[] shipLists;

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

    //private void OnValidate()
    //{
    //    string pathPrefix = "Assets/Resources/";

    //    string[] shipDirectories = Directory.GetDirectories(pathPrefix + "Ships");

    //    for (int i = 0; i < shipDirectories.Length; i++)
    //    {
    //        shipDirectories[i] = shipDirectories[i].Replace(pathPrefix, string.Empty);
    //        shipDirectories[i] = shipDirectories[i].Replace("\\", "/");
    //    }
        

    //    shipLists = new ShipList[shipDirectories.Length];

    //    for (int i = 0; i < shipLists.Length; i++)
    //    {
    //        shipLists[i] = new ShipList();
    //        shipLists[i].name = shipDirectories[i].Replace("Ships/", string.Empty);

    //        shipLists[i].ships = Resources.LoadAll<Ship>(shipDirectories[i]);
    //    }
    //}

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

//public class ShipList
//{
//    public string name;
//    public Ship[] ships;
//}