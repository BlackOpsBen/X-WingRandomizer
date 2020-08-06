using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideScreens : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject listsScreen;
    [SerializeField] private GameObject mainButtons;

    bool mainButtonsShouldBe;

    private void Awake()
    {
        settingsScreen.SetActive(false);
        listsScreen.SetActive(false);
    }

    public void ToggleSettingsScreen()
    {
        if (!settingsScreen.activeSelf)
        {
            mainButtonsShouldBe = mainButtons.activeSelf;
            settingsScreen.SetActive(true);
            listsScreen.SetActive(false);
            mainButtons.SetActive(false);
        }
        else
        {
            settingsScreen.SetActive(false);
            mainButtons.SetActive(true);
        }
    }

    public void ToggleListScreen()
    {
        if (!listsScreen.activeSelf)
        {
            mainButtonsShouldBe = mainButtons.activeSelf;
            listsScreen.SetActive(true);
            settingsScreen.SetActive(false);
            mainButtons.SetActive(false);
        }
        else
        {
            listsScreen.SetActive(false);
            mainButtons.SetActive(true);
        }
    }
}
