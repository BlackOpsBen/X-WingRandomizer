using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PilotCard : ScriptableObject
{
    private AddonType[] addonTypes = new AddonType[17];

    [Header("Settings")]
    [SerializeField] private Sprite cardArt;
    [SerializeField] private bool isUnique;
    [SerializeField] private int pilotSkill;
    [SerializeField] private int cost;

    [Header("Common addons")]
    [SerializeField] private int elitePilotTalents;
    [SerializeField] private int torpedoes;
    [SerializeField] private int missiles;
    [SerializeField] private int bombs;
    [SerializeField] private int modifications = 1;

    [Header("Uncommon addons")]
    [SerializeField] private int astromechs;
    [SerializeField] private int cannons;
    [SerializeField] private int turrets;
    [SerializeField] private int crews;
    [SerializeField] private int systems;
    [SerializeField] private int techs;

    [Header("Rare addons")]
    [SerializeField] private int illicits;
    [SerializeField] private int cargos;
    [SerializeField] private int hardpoints;
    [SerializeField] private int teams;
    [SerializeField] private int salvagedAstromechs;

    public void MakeList()
    {
        for (int i = 0; i < addonTypes.Length; i++)
        {
            addonTypes[i] = new AddonType();
        }
        addonTypes[0].name = "Elite Pilot Talents";
        addonTypes[0].quantity = elitePilotTalents;

        addonTypes[1].name = "Torpedoes";
        addonTypes[1].quantity = torpedoes;

        addonTypes[2].name = "Missiles";
        addonTypes[2].quantity = missiles;

        addonTypes[3].name = "Bombs";
        addonTypes[3].quantity = bombs;

        addonTypes[4].name = "Modifications";
        addonTypes[4].quantity = modifications;

        addonTypes[5].name = "Astromechs";
        addonTypes[5].quantity = astromechs;

        addonTypes[6].name = "Cannons";
        addonTypes[6].quantity = cannons;

        addonTypes[7].name = "Turrets";
        addonTypes[7].quantity = turrets;

        addonTypes[8].name = "Crews";
        addonTypes[8].quantity = crews;

        addonTypes[9].name = "Systems";
        addonTypes[9].quantity = systems;

        addonTypes[10].name = "Techs";
        addonTypes[10].quantity = techs;

        addonTypes[11].name = "Illicits";
        addonTypes[11].quantity = illicits;

        addonTypes[12].name = "Cargos";
        addonTypes[12].quantity = cargos;

        addonTypes[13].name = "Hardpoints";
        addonTypes[13].quantity = hardpoints;

        addonTypes[14].name = "Teams";
        addonTypes[14].quantity = teams;

        addonTypes[15].name = "SalvagedAstromechs";
        addonTypes[15].quantity = salvagedAstromechs;
    }
    private struct AddonType
    {
        public string name;
        public int quantity;
    }

    public int GetAddonTypeQuantity(int i)
    {
        return addonTypes[i].quantity;
    }

    public int GetNumAddonTypes()
    {
        return addonTypes.Length;
    }

    public int GetCost()
    {
        return cost;
    }

    public Texture GetTexture()
    {
        return cardArt.texture;
    }

    public int GetPilotSkill()
    {
        return pilotSkill;
    }
}