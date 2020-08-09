using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayProductToggles : MonoBehaviour
{
    public static DisplayProductToggles Instance { get; private set; }

    private const float refScreenWidth = 1920f;
    private const float refScreenHeight = 1080f;

    [SerializeField] private GameObject toggleObject;
    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private RectTransform SelectAllButton;
    [SerializeField] private Vector2 SelectAllOffset;
    [SerializeField] private float listStartXPos = 190f;
    [SerializeField] private float listStartYPos = 913.5f;
    [SerializeField] private float horizontalSpacing = 441.8f;
    [SerializeField] private float verticalSpacing = -54.95f;
    [SerializeField] private int linesPerColumnLimit = 14;
    [SerializeField] private float nonclickablePadding = 175f;


    private Vector2 scaledSelectAllOffset;
    private float scaledListStartXPos;
    private float scaledListStartYPos;
    private float scaledHorizontalSpacing;
    private float scaledVerticalSpacing;
    private float scaledNonclickablePadding;

    [SerializeField] private Color checkedColor;
    [SerializeField] private Color uncheckedColor;
    [SerializeField] private Color highlightedColor;

    private Vector3 startPos;
    private Vector3 offset;

    private ProductToggle[] productToggles;

    public bool toggleSoundEnabled = true;

    private void Awake()
    {
        SingletonPattern();

        CreateToggles();
    }

    private void Start()
    {
        LoadSettings();
        AudioManager.Instance.SetIsReadyToPlay();
    }

    private void LoadSettings()
    {
        toggleSoundEnabled = false;
        for (int i = 0; i < productToggles.Length; i++)
        {
            productToggles[i].LoadToggleSetting();
        }
        toggleSoundEnabled = true;
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void ScaleValuesWithScreen()
    {
        Vector2 selectOffsetPercents = new Vector2(SelectAllOffset.x / refScreenWidth, SelectAllOffset.y / refScreenHeight);
        scaledSelectAllOffset = new Vector2(Screen.width * selectOffsetPercents.x, Screen.height * selectOffsetPercents.y);

        float startXPercent = listStartXPos / refScreenWidth;
        scaledListStartXPos = Screen.width * startXPercent;

        float startYPercent = listStartYPos / refScreenHeight;
        scaledListStartYPos = Screen.height * startYPercent;

        float horSpacePercent = horizontalSpacing / refScreenWidth;
        scaledHorizontalSpacing = Screen.width * horSpacePercent;

        float vertSpacePercent = verticalSpacing / refScreenHeight;
        scaledVerticalSpacing = Screen.height * vertSpacePercent;

        float paddingPercent = nonclickablePadding / refScreenWidth;
        scaledNonclickablePadding = Screen.width * paddingPercent;
    }

    private void Update()
    {
        ScaleValuesWithScreen();

        UpdateArrangement();
    }

    public void CreateToggles()
    {
        List<Product> products = new List<Product>(Resources.LoadAll<Product>("Products"));

        productToggles = new ProductToggle[products.Count];

        for (int i = 0; i < products.Count; i++)
        {
            productToggles[i] = new ProductToggle(Instantiate(toggleObject, parentCanvas.transform), products[i].name);
        }
    }

    private void UpdateArrangement()
    {
        startPos = new Vector3(scaledListStartXPos, scaledListStartYPos);
        offset = new Vector3(scaledHorizontalSpacing, scaledVerticalSpacing);

        SelectAllButton.position = startPos + new Vector3(scaledSelectAllOffset.x, scaledSelectAllOffset.y);

        for (int i = 0; i < productToggles.Length; i++)
        {
            productToggles[i].UpdateUIPosition(startPos, offset, linesPerColumnLimit, i, nonclickablePadding, horizontalSpacing);
            productToggles[i].SetColors(checkedColor, uncheckedColor, highlightedColor);
        }
    }

    public void SelectAllNone()
    {
        toggleSoundEnabled = false;
        bool allOn = DetermineIfAllOn();

        if (allOn)
        {
            TurnAll(false);
        }
        else
        {
            TurnAll(true);
        }
        UIManager.Instance.UpdateUI();
        toggleSoundEnabled = true;
    }

    private void TurnAll(bool value)
    {
        foreach (ProductToggle pToggle in productToggles)
        {
            pToggle.ManualToggle(value);
        }
    }

    private bool DetermineIfAllOn()
    {
        for (int i = 0; i < productToggles.Length; i++)
        {
            if (productToggles[i].GetIsEnabled() == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool GetIsEnabled(IComeInProducts item)
    {
        foreach (Product productRequired in item.GetProductsIncludedWith())
        {
            foreach (ProductToggle productToggle in productToggles)
            {
                if (productToggle.GetIsEnabled() && productToggle.GetName() == productRequired.name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

public class ProductToggle
{
    private string name;
    private bool isEnabled = true;
    private GameObject toggleObject;

    private RectTransform rectTransform;
    private Toggle toggle;

    private ColorBlock cb;
    private Color normalColor;
    private Color uncheckedColor;
    private Color highlightedColor;

    public ProductToggle(GameObject obj, string label)
    {
        SetToggleObject(obj);
        SetNameAndLabel(label);
        SubscribeToToggleState();

        rectTransform = toggleObject.GetComponent<RectTransform>();
        toggle = toggleObject.GetComponent<Toggle>();
        cb = toggle.colors;
        cb.normalColor = normalColor;
        cb.highlightedColor = highlightedColor;
        toggle.colors = cb;
    }

    private void SetNameAndLabel(string label)
    {
        name = label;
        toggleObject.GetComponentInChildren<TextMeshProUGUI>().text = label;
    }

    public string GetName()
    {
        return name;
    }

    private void SubscribeToToggleState()
    {
        toggleObject.GetComponent<Toggle>().onValueChanged.AddListener(SetIsEnabled);
    }

    private void SetIsEnabled(bool value)
    {
        isEnabled = value;

        ToggleVisualState(value);

        UIManager.Instance.UpdateUI();

        SaveToggleSetting();

        if (DisplayProductToggles.Instance.toggleSoundEnabled)
        {
            AudioManager.Instance.Play("Check");
        }
    }

    public bool GetIsEnabled()
    {
        return isEnabled;
    }

    private void SetToggleObject(GameObject obj)
    {
        toggleObject = obj;
    }

    public void UpdateUIPosition(Vector3 scaledStartPos, Vector3 scaledOffset, int lineLimit, int index, float unscaledPadding, float unscaledXOffset)
    {
        rectTransform.SetPositionAndRotation(scaledStartPos + new Vector3(scaledOffset.x * Mathf.Floor(index / lineLimit), scaledOffset.y * (index % lineLimit)), Quaternion.identity);
        rectTransform.GetComponentInChildren<AdjustLabelWidth>().SetWidth(unscaledXOffset - unscaledPadding);
    }

    private void ToggleVisualState(bool value)
    {
        cb.highlightedColor = highlightedColor;
        if (value)
        {
            cb.normalColor = normalColor;
        }
        else
        {
            cb.normalColor = uncheckedColor;
        }
        toggle.colors = cb;
    }

    public void SetColors(Color on, Color off, Color highlighted)
    {
        normalColor = on;
        uncheckedColor = off;
        highlightedColor = highlighted;
        ToggleVisualState(isEnabled);
    }

    public void ManualToggle(bool value)
    {
        toggle.isOn = value;
        isEnabled = value;
        ToggleVisualState(value);
        SaveToggleSetting();
    }
    private void SaveToggleSetting()
    {
        int flag;
        if (isEnabled)
        {
            flag = 1;
        }
        else
        {
            flag = 0;
        }
        PlayerPrefs.SetInt(this.name, flag);
        PlayerPrefs.Save();
    }

    public void LoadToggleSetting()
    {
        int flag = PlayerPrefs.GetInt(this.name);

        if (flag == 1)
        {
            ManualToggle(true);
        }
        else
        {
            ManualToggle(false);
        }
    }
}