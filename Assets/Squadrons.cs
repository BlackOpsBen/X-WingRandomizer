using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squadrons : MonoBehaviour
{
    public static Squadrons Instance { get; private set; }

    [SerializeField] private Squadron[] squadrons;

    private void Awake()
    {
        SingletonPattern();
    }

    private void Start()
    {
        InitializeSquadrons();
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

    private void InitializeSquadrons()
    {
        squadrons = new Squadron[PilotCardManager.Instance.factions.Length];
        for (int i = 0; i < squadrons.Length; i++)
        {
            squadrons[i] = new Squadron();
            squadrons[i].factionName = PilotCardManager.Instance.factions[i];
            squadrons[i].pilotSets = new List<PilotSet>();
        }
    }

    public void RecordPilotSet(Ship ship, PilotCard pilot, AddonCard[] addonCards)
    {
        PilotSet pilotSet = new PilotSet
        {
            shipName = ship.name,
            pilotName = pilot.name,
            pilotCost = pilot.GetCost(),
            selectedAddons = new SelectedAddon[addonCards.Length]
        };

        for (int i = 0; i < addonCards.Length; i++)
        {
            pilotSet.selectedAddons[i] = new SelectedAddon();
            pilotSet.selectedAddons[i].addonName = addonCards[i].name;
            pilotSet.selectedAddons[i].cost = addonCards[i].cost;
        }

        CalculateSetTotalCost(pilotSet);

        squadrons[PilotCardManager.Instance.selectedFaction].pilotSets.Add(pilotSet);

        CalculateSquadronTotalCost(squadrons[PilotCardManager.Instance.selectedFaction]);
    }

    private void CalculateSetTotalCost(PilotSet pilotSet)
    {
        pilotSet.pilotSetCost = pilotSet.pilotCost;
        for (int i = 0; i < pilotSet.selectedAddons.Length; i++)
        {
            pilotSet.pilotSetCost += pilotSet.selectedAddons[i].cost;
        }
    }

    private void CalculateSquadronTotalCost(Squadron squadron)
    {
        squadron.totalCost = 0;
        foreach (PilotSet pilotSet in squadron.pilotSets)
        {
            squadron.totalCost += pilotSet.pilotSetCost;
        }
    }

    [System.Serializable]
    private class Squadron
    {
        public string factionName;
        public List<PilotSet> pilotSets;
        public int totalCost;
    }

    private class PilotSet
    {
        public string shipName;
        public string pilotName;
        public int pilotCost;
        public SelectedAddon[] selectedAddons;
        public int pilotSetCost;
    }

    private class SelectedAddon
    {
        public string addonName;
        public int cost;
    }

    public int GetSquadronTotalCost(string faction)
    {
        for (int i = 0; i < squadrons.Length; i++)
        {
            if (squadrons[i].factionName == faction)
            {
                return squadrons[i].totalCost;
            }
        }
        Debug.LogError("No Squadron cost was found. Returning 0.");
        return 0;
    }

    public int GetSquadronTotalCost(int factionIndex)
    {
        return squadrons[factionIndex].totalCost;
    }
}
