using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMuteIcon : MonoBehaviour
{
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image unmuteIcon;
    public void ToggleIcon(bool state)
    {
        muteIcon.enabled = state;
        unmuteIcon.enabled = !state;
    }
}
