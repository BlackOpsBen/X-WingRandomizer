using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRemoveBadge : MonoBehaviour
{
    private GameObject badge;
    private ToggleBadgeVisibility visToggle;

    private MoveAndFlip moveAndFlip;

    private void Awake()
    {
        moveAndFlip = GetComponent<MoveAndFlip>();
    }

    private void Update()
    {
        if (moveAndFlip != null && badge != null)
        {
            if (moveAndFlip.hasArrived)
            {
                SetBadgeActive(true);
            }
            else
            {
                SetBadgeActive(false);
            }
        }
    }

    private void SetBadgeActive(bool value)
    {
        visToggle.SetBadgeVisibility(value);
    }

    public void SetBadge(GameObject badgeObject)
    {
        badge = badgeObject;
        visToggle = badge.GetComponent<ToggleBadgeVisibility>();
    }
}
