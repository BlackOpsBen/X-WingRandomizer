using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRemoveBadges : MonoBehaviour
{
    [SerializeField] private GameObject badgeObject;

    [SerializeField] private GameObject badgeParentCanvas;

    private List<GameObject> badges = new List<GameObject>();

}
