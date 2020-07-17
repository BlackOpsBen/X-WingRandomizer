using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableGenerate : MonoBehaviour
{
    [SerializeField] private Button generateButton;

    public void SetGenerateButton()
    {
        if (PilotCardManager.Instance.GetAffordableShips(Squadrons.Instance.GetPointsRemaining()).Count == 0)
        {
            generateButton.interactable = false;
        }
        else
        {
            generateButton.interactable = true;
        }
    }
}
