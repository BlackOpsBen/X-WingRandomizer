using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableGenerate : MonoBehaviour
{
    [SerializeField] private Button generateButton;
    private TextMeshProUGUI buttonLabel;

    [SerializeField] private string generateText = "GENERATE PILOT";
    [SerializeField] private string insufficientText = "NO VALID OPTIONS";

    private void Awake()
    {
        buttonLabel = generateButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetGenerateButton()
    {
        if (PilotCardManager.Instance.GetAffordableShips(Squadrons.Instance.GetPointsRemaining()).Count == 0)
        {
            generateButton.interactable = false;
            buttonLabel.text = insufficientText;
        }
        else
        {
            generateButton.interactable = true;
            buttonLabel.text = generateText;
        }
    }
}
