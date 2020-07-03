using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddonCardManager : MonoBehaviour
{
    public static AddonCardManager Instance { get; private set; }

    private string[] addonCardNames;
    public AddonCardGroup[] addonCardGroups;

    //public ElitePilotTalent[] elitePilotTalents;
    //public Torpedo[] torpedos;
    //public Missile[] missiles;
    ////public Bomb[] bombs;
    //public Modification[] modifications;
    //public Title[] titles;
    //public Astromech[] astromechs;
    ////public Cannon[] cannons;
    ////public Turret[] turrets;
    ////public Crew[] crews;
    ////public System[] systems;
    ////public Tech[] techs;
    ////public Illicit[] illicits;
    ////public Cargo[] cargos;
    ////public Hardpoint[] hardpoints;
    ////public Team[] teams;
    ////public SalvagedAstromech[] salvagedAstromechs;

    private void Awake()
    {
        SingletonPattern();

        addonCardNames = new string[]
        {
            "Elite Pilot Talents",
            "Torpedos",
            "Missiles",
            "Bombs",
            "Modifications",
            "Titles",
            "Astromechs",
            "Cannons",
            "Turrets",
            "Crews",
            "Systems",
            "Techs",
            "Illicits",
            "Cargos",
            "Hardpoints",
            "Teams",
            "SalvagedAstromechs"
        };

        addonCardGroups = new AddonCardGroup[addonCardNames.Length];

        for (int i = 0; i < addonCardGroups.Length; i++)
        {
            addonCardGroups[i] = new AddonCardGroup();
        }

        for (int i = 0; i < addonCardGroups.Length; i++)
        {
            addonCardGroups[i].name = addonCardNames[i];
            addonCardGroups[i].addonCards = Resources.LoadAll<AddonCard>(addonCardNames[i]);
        }
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

[System.Serializable]
public class AddonCardGroup
{
    public string name;
    public AddonCard[] addonCards;
}
