using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public Ship[] ships;
    public PilotCard[] pilots;
    public Title[] titles;
    public Astromech[] astromechs;
    public Missile[] missiles;
    public Torpedo[] torpedoes;
    public ElitePilotTalent[] elitePilotTalents;
    public Modification[] modifications;

    public PilotGroup[] pilotGroups;

    private void Awake()
    {
        SingletonPattern();
        PrepareShipLists();
        SortPilots();
    }

    private void PrepareShipLists()
    {
        pilotGroups = new PilotGroup[ships.Length];
        for (int i = 0; i < pilotGroups.Length; i++)
        {
            pilotGroups[i] = new PilotGroup();
            pilotGroups[i].shipPilots = new List<PilotCard>();
        }
    }

    private void SortPilots()
    {
        for (int i = 0; i < ships.Length; i++)
        {
            foreach (PilotCard pilot in pilots)
            {
                if (pilot.ship == ships[i].name)
                {
                    pilotGroups[i].shipPilots.Add(pilot);
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
}

[System.Serializable]
public class PilotGroup
{
    public List<PilotCard> shipPilots;
}