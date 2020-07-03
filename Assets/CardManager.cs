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
        CreatePilotGroups();
    }

    private void CreatePilotGroups()
    {
        rebelPilotGroups = new PilotGroup[rebelShips.Length];

        for (int i = 0; i < rebelPilotGroups.Length; i++)
        {
            rebelPilotGroups[i] = new PilotGroup();
        }

        for (int i = 0; i < rebelPilotGroups.Length; i++)
        {
            string path = "Pilots/" + rebelShips[i].name;
            rebelPilotGroups[i].pilots = Resources.LoadAll<PilotCard>(path);
        }
    }

    private void LoadAllShips()
    {
        rebelShips = Resources.LoadAll<Ship>("Ships/Rebel");
        imperialShips = Resources.LoadAll<Ship>("Ships/Imperial");
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