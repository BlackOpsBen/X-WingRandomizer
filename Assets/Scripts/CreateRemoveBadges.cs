using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRemoveBadges : MonoBehaviour
{
    [SerializeField] private GameObject badgeObject;

    [SerializeField] private Transform badgeParentCanvas;

    private List<GameObject> badges = new List<GameObject>();

    private CardRandomizer cardRandomizer;

    private void Awake()
    {
        cardRandomizer = GetComponent<CardRandomizer>();
    }

    public void CreateNewBadge(GameObject cardObject)
    {
        GameObject newBadge = Instantiate(badgeObject, badgeParentCanvas);
        
        newBadge.GetComponent<UIWorldToScreenPos>().SetWorldTransform(cardObject.transform.GetChild(0));

        newBadge.GetComponent<Button>().onClick.AddListener(() => UserRemoveAddon(newBadge));
        
        badges.Add(newBadge);

        EnableRemoveBadge enableRemoveBadge = cardObject.AddComponent<EnableRemoveBadge>();
        enableRemoveBadge.SetBadge(newBadge);
    }

    public void UserRemoveAddon(GameObject selfBadge)
    {
        int index = badges.IndexOf(selfBadge);
        badges.RemoveAt(index);
        cardRandomizer.RemoveAddon(index);
    }

    public void ResetBadges()
    {
        foreach (GameObject badge in badges)
        {
            badge.GetComponent<BadgeDestroySelf>().DestroySelf();
        }
        badges.Clear();
    }


    //private void DebugIndexes()
    //{
    //    for (int i = 0; i < badges.Count; i++)
    //    {
    //        badges[i].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
    //    }
    //}
}
