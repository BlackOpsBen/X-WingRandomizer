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

    private List<GameObject>[] factionListItemGroups;

    private void Awake()
    {
        factionListItemGroups = new List<GameObject>[3];
        for (int i = 0; i < factionListItemGroups.Length; i++)
        {
            factionListItemGroups[i] = new List<GameObject>();
        }
    }

    public void AddPilotSet(string shipName, string pilotName, AddonCard[] addons, int factionIndex)
    {
        GameObject newSet = Instantiate(pilotSetObject, contentContainers[factionIndex].transform);

        factionListItemGroups[factionIndex].Add(newSet);

        newSet.GetComponentInChildren<TextMeshProUGUI>().text = pilotName + " (" + shipName + ")";
        for (int i = 0; i < addons.Length; i++)
        {
            GameObject newAddon = Instantiate(addonTextObject, newSet.transform);

            newAddon.GetComponent<TextMeshProUGUI>().text = "+ " + addons[i].name + " (" + addons[i].GetType().ToString() + ")";

            factionListItemGroups[factionIndex].Add(newAddon);
        }
    }

    public void ResetFaction(int factionIndex)
    {
        foreach (GameObject listObject in factionListItemGroups[factionIndex])
        {
            Destroy(listObject);
        }
        factionListItemGroups[factionIndex] = new List<GameObject>();

        Squadrons.Instance.ResetFaction(factionIndex);
    }

    public void ResetAll()
    {
        ResetFaction(0);
        ResetFaction(1);
        ResetFaction(2);
    }
}
