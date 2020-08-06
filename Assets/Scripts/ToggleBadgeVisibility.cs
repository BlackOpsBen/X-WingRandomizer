using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleBadgeVisibility : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private Button button;
    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        SetBadgeVisibility(false);
    }

    public void SetBadgeVisibility(bool value)
    {
        if (value)
        {
            image.color = Color.red;
            text.text = "x";
            button.interactable = true;
        }
        else
        {
            image.color = Color.clear;
            text.text = "";
            button.interactable = false;
        }
    }
}
