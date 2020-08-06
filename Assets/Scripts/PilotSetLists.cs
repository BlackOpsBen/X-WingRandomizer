using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PilotSetLists : MonoBehaviour
{
    [Header("Prefab Objects")]
    [SerializeField] private GameObject pilotSetObject;
    [SerializeField] private GameObject addonTextObject;

    [Header("Content Containers")]
    [SerializeField] private GameObject[] contentContainers;

    public void AddPilotSet(string shipName, string pilotName, AddonCard[] addons, int factionIndex)
    {
        GameObject newSet = Instantiate(pilotSetObject, contentContainers[factionIndex].transform);
        newSet.GetComponentInChildren<TextMeshProUGUI>().text = pilotName + " (" + shipName + ")";
        for (int i = 0; i < addons.Length; i++)
        {
            GameObject newAddon = Instantiate(addonTextObject, newSet.transform);

            newAddon.GetComponent<TextMeshProUGUI>().text = "+ " + addons[i].name + " (" + addons[i].GetType().ToString() + ")";
        }
    }
}
