using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionIcon : MonoBehaviour
{
    [SerializeField] private Sprite rebelIcon;
    [SerializeField] private Sprite imperialIcon;
    [SerializeField] private Sprite scumIcon;

    private Sprite[] icons = new Sprite[3];

    private Image uiImage;

    private void Awake()
    {
        icons[0] = rebelIcon;
        icons[1] = imperialIcon;
        icons[2] = scumIcon;

        uiImage = GetComponent<Image>();
    }

    public void SetIcon()
    {
        int factionIndex = PilotCardManager.Instance.GetSelectedFactionIndex();
        uiImage.sprite = icons[factionIndex];
    }
}
