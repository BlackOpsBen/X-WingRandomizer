using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsRemainingCounter;

    private void Start()
    {
        pointsRemainingCounter.text = 0 + "/" + Settings.Instance.GetPointLimit();
    }

    public void UpdatePoints()
    {
        int pointsSpent = Squadrons.Instance.GetSquadronTotalCost(PilotCardManager.Instance.GetSelectedFactionIndex());
        int pointLimit = Settings.Instance.GetPointLimit();
        pointsRemainingCounter.text = pointsSpent + "/" + pointLimit;
    }
}
