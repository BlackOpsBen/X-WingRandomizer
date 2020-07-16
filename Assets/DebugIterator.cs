using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugIterator : MonoBehaviour
{
    [SerializeField] public int shipIndex = 0;
    [SerializeField] public int pilotIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetAllShipsAndPilots();
        }
    }

    private void GetShipAndPilotDetails()
    {
        Ship ship = PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].ships[shipIndex];

        PilotCard pilot = PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].pilotGroups[shipIndex].pilots[pilotIndex];
        
        LogNames(ship, pilot);
    }

    private void LogNames(Ship ship, PilotCard pilot)
    {
        LogShipName(ship);
        LogPilotName(pilot);
    }

    private void LogPilotName(PilotCard pilot)
    {
        Debug.Log("Pilot: " + pilot.name);
    }

    private void LogShipName(Ship ship)
    {
        Debug.LogWarning("Ship: " + ship.name);
    }

    private void GetAllShipsAndPilots()
    {
        Ship[] ships = PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].ships;
        for (int i = 0; i < ships.Length; i++)
        {
            Ship ship = ships[i];
            LogShipName(ship);

            PilotCard[] pilots = PilotCardManager.Instance.factionList[PilotCardManager.Instance.GetSelectedFactionIndex()].pilotGroups[i].pilots;
            for (int j = 0; j < pilots.Length; j++)
            {
                PilotCard pilot = pilots[j];
                LogPilotName(pilot);
            }
        }
    }
}
