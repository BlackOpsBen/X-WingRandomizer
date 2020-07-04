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
    [SerializeField] private int cost;

    [Header("Common addons")]
    [SerializeField] private int elitePilotTalents;
    [SerializeField] private int torpedos;
    [SerializeField] private int missiles;
    [SerializeField] private int bombs;
    [SerializeField] private int modifications = 1;

    [Header("Uncommon addons")]
    [SerializeField] private int titles;
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

        addonTypes[1].name = "Torpedos";
        addonTypes[1].quantity = torpedos;

        addonTypes[2].name = "Missiles";
        addonTypes[2].quantity = missiles;

        addonTypes[3].name = "Bombs";
        addonTypes[3].quantity = bombs;

        addonTypes[4].name = "Modifications";
        addonTypes[4].quantity = modifications;

        addonTypes[5].name = "Titles";
        addonTypes[5].quantity = titles;

        addonTypes[6].name = "Astromechs";
        addonTypes[6].quantity = astromechs;

        addonTypes[7].name = "Cannons";
        addonTypes[7].quantity = cannons;

        addonTypes[8].name = "Turrets";
        addonTypes[8].quantity = turrets;

        addonTypes[9].name = "Crews";
        addonTypes[9].quantity = crews;

        addonTypes[10].name = "Systems";
        addonTypes[10].quantity = systems;

        addonTypes[11].name = "Techs";
        addonTypes[11].quantity = techs;

        addonTypes[12].name = "Illicits";
        addonTypes[12].quantity = illicits;

        addonTypes[13].name = "Cargos";
        addonTypes[13].quantity = cargos;

        addonTypes[14].name = "Hardpoints";
        addonTypes[14].quantity = hardpoints;

        addonTypes[15].name = "Teams";
        addonTypes[15].quantity = teams;

        addonTypes[16].name = "SalvagedAstromechs";
        addonTypes[16].quantity = salvagedAstromechs;
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
}