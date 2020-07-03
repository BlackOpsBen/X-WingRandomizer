using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public Ship[] rebelShips;
    public Ship[] imperialShips;
    public PilotGroup[] rebelPilotGroups;
    public PilotGroup[] imperialPilotGroups;

    private void Awake()
    {
        SingletonPattern();

        LoadAllShips();
        CreatePilotGroups(ref rebelPilotGroups, ref rebelShips);
        CreatePilotGroups(ref imperialPilotGroups, ref imperialShips);
    }

    private void LoadAllShips()
    {
        rebelShips = Resources.LoadAll<Ship>("Ships/Rebel");
        imperialShips = Resources.LoadAll<Ship>("Ships/Imperial");
    }

    private void CreatePilotGroups(ref PilotGroup[] pilotGroups, ref Ship[] ships)
    {
        pilotGroups = new PilotGroup[ships.Length];

        for (int i = 0; i < pilotGroups.Length; i++)
        {
            pilotGroups[i] = new PilotGroup();
        }

        for (int i = 0; i < pilotGroups.Length; i++)
        {
            string path = "Pilots/" + ships[i].name;
            pilotGroups[i].pilots = Resources.LoadAll<PilotCard>(path);
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
    public PilotCard[] pilots;
}