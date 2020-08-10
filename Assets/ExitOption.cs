using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOption : MonoBehaviour
{
    [SerializeField] private GameObject exitDialogScreen;

    private void Awake()
    {
        HideExitDialog();
    }

    public void ShowExitDialog()
    {
        exitDialogScreen.SetActive(true);
    }

    public void HideExitDialog()
    {
        exitDialogScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
