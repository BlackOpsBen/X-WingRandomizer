using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomizer : MonoBehaviour
{
    public Ship ship;
    public PilotCard pilot;
    public List<AddonCard> addonCards;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {            
            MakeRandomPilot();
            SelectAddons();
        }
    }

    private void MakeRandomPilot()
    {
        int randShip = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelShips.Length);
        ship = PilotCardManager.Instance.rebelShips[randShip];
        Debug.Log(ship.name);

        int randPilot = UnityEngine.Random.Range(0, PilotCardManager.Instance.rebelPilotGroups[randShip].pilots.Length);
        pilot = PilotCardManager.Instance.rebelPilotGroups[randShip].pilots[randPilot];
        Debug.Log(pilot.name);

        pilot.MakeList();
    }

    private void SelectAddons()
    {

    }
}
