using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRandomCards : MonoBehaviour
{
    public Ship ship;
    public PilotCard pilot;
    public AddonCard[] addons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Randomizing...");
            MakeRandomPilot();
        }
    }

    private void MakeRandomPilot()
    {
        int randShip = UnityEngine.Random.Range(0, CardManager.Instance.rebelShips.Length);
        ship = CardManager.Instance.rebelShips[randShip];
        Debug.Log(ship.name);

        int randPilot = UnityEngine.Random.Range(0, CardManager.Instance.rebelPilotGroups[randShip].pilots.Length);
        pilot = CardManager.Instance.rebelPilotGroups[randShip].pilots[randPilot];
        Debug.Log(pilot.name);

        pilot.GetAddonSlots();
    }
}
