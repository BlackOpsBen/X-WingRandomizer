using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideSettingsScreen : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject mainButtons;

    bool mainButtonsShouldBe;

    private void Awake()
    {
        settingsScreen.SetActive(false);
    }

    public void ToggleSettingsScreen()
    {
        if (!settingsScreen.activeSelf)
        {
            mainButtonsShouldBe = mainButtons.activeSelf;
            settingsScreen.SetActive(true);
            mainButtons.SetActive(false);
        }
        else
        {
            settingsScreen.SetActive(false);
            mainButtons.SetActive(mainButtonsShouldBe);
        }

        AudioManager.Instance.Play("Settings");
    }
}
