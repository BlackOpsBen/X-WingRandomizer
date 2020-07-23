using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideSettingsScreen : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;

    public void ToggleSettingsScreen()
    {
        settingsScreen.SetActive(!settingsScreen.activeSelf);
    }
}
