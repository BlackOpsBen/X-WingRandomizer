using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddonCardManager : MonoBehaviour
{
    public static AddonCardManager Instance { get; private set; }

    public string[] addonCardNames { get; private set; }
    [SerializeField] private AddonCardGroup[] addonCardGroups;

    private void Awake()
    {
        SingletonPattern();

        InitializeNames();
        CreateAddonCardGroups();
    }

    private void InitializeNames()
    {
        addonCardNames = new string[]
            {
            "ElitePilotTalent",
            "Torpedo",
            "Missile",
            "Bomb",
            "Modification",
            "Astromech",
            "Cannon",
            "Turret",
            "Crew",
            "SystemUpgrade",
            "Tech",
            "Illicit",
            "SalvagedAstromech"
            };
    }

    private void CreateAddonCardGroups()
    {
        addonCardGroups = new AddonCardGroup[addonCardNames.Length];

        for (int i = 0; i < addonCardGroups.Length; i++)
        {
            addonCardGroups[i] = new AddonCardGroup();
            addonCardGroups[i].name = addonCardNames[i];
            addonCardGroups[i].addonCards = Resources.LoadAll<AddonCard>(addonCardNames[i]);
        }
    }

    public AddonCard GetAddonCard(int group, int card)
    {
        return addonCardGroups[group].addonCards[card];
    }

    public int GetAddonCardGroupLength(int group)
    {
        return addonCardGroups[group].addonCards.Length;
    }

    public int GetNumAddonTypes()
    {
        return addonCardNames.Length;
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
