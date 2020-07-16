using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotCardManager : MonoBehaviour
{
    public static PilotCardManager Instance { get; private set; }

    public string[] factions;

    public int selectedFaction;

    private GameObject pilotCard;

    [System.Serializable]
    public class Faction
    {
        public string name;
        public Ship[] ships;
        public PilotGroup[] pilotGroups;
    }

    public Faction[] factionList;

    private void Awake()
    {
        SingletonPattern();

        pilotCard = FindObjectOfType<ChangeFaction>().gameObject;

        InitializeFactions();

        LoadAllShips();

        LoadAllPilots();
    }

    private void InitializeFactions()
    {
        factionList = new Faction[factions.Length];
        for (int i = 0; i < factionList.Length; i++)
        {
            factionList[i] = new Faction();
            factionList[i].name = factions[i];
        }
    }

    private void LoadAllShips()
    {
        for (int i = 0; i < factionList.Length; i++)
        {
            factionList[i].ships = Resources.LoadAll<Ship>("Ships/" + factionList[i].name);
        }
    }

    private void LoadAllPilots()
    {
        for (int i = 0; i < factionList.Length; i++)
        {
            CreatePilotGroups(ref factionList[i].pilotGroups, ref factionList[i].ships);
        }
    }

    private void CreatePilotGroups(ref PilotGroup[] pilotGroups, ref Ship[] ships)
    {
        pilotGroups = new PilotGroup[ships.Length];

        for (int i = 0; i < pilotGroups.Length; i++)
        {
            pilotGroups[i] = new PilotGroup();
            string path = "Pilots/" + ships[i].name;
            pilotGroups[i].name = ships[i].name;
            pilotGroups[i].pilots = Resources.LoadAll<PilotCard>(path);
        }

        FindCheapestPilotCost(pilotGroups, ships);
    }

    private static void FindCheapestPilotCost(PilotGroup[] pilotGroups, Ship[] ships)
    {
        for (int i = 0; i < ships.Length; i++)
        {
            int cheapestCost = 100;
            for (int j = 0; j < pilotGroups[i].pilots.Length; j++)
            {
                if (pilotGroups[i].pilots[j].GetCost() < cheapestCost)
                {
                    ships[i].SetCheapestPilotCost(cheapestCost);
                }
            }
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

    public void ChangeSelectedFaction(int faction)
    {
        selectedFaction = faction;
        pilotCard.GetComponent<ChangeFaction>().ChangeCardBack(selectedFaction);
    }
}

[System.Serializable]
public class PilotGroup
{
    public string name;
    public PilotCard[] pilots;
}