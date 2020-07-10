using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squadrons : MonoBehaviour
{
    public static Squadrons Instance { get; private set; }

    private Squadron[] squadrons;

    private void Awake()
    {
        SingletonPattern();
        
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

    private struct Squadron
    {
        public List<PilotSet> pilotSets;
    }

    private struct PilotSet
    {
        public string ship;
        
        public string pilot;
        public int pilotCost;

        public SelectedAddon[] selectedAddons;
    }

    private struct SelectedAddon
    {
        public string name;
        public int cost;
    }
}
