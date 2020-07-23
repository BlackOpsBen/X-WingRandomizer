using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayProductToggles : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private float listStartXPos = 190f;
    [SerializeField] private float listStartYPos = 913.5f;
    [SerializeField] private float horizontalSpacing = 441.8f;
    [SerializeField] private float verticalSpacing = -54.95f;
    [SerializeField] private int linesPerColumnLimit = 14;
    [SerializeField] private float nonclickablePadding = 175f;

    [SerializeField] private Color checkedColor;
    [SerializeField] private Color uncheckedColor;
    [SerializeField] private Color highlightedColor;

    private Vector3 startPos;
    private Vector3 offset;

    private ProductToggle[] productToggles;

    //[SerializeField] private List<GameObject> toggles = new List<GameObject>();

    private void Awake()
    {
        CreateToggles();
    }

    private void Update()
    {
        UpdateArrangement(); // TODO move this to Awake instead of Update
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
        startPos = new Vector3(listStartXPos, listStartYPos);
        offset = new Vector3(horizontalSpacing, verticalSpacing);

        for (int i = 0; i < productToggles.Length; i++)
        {
            productToggles[i].UpdateUIPosition(startPos, offset, linesPerColumnLimit, i, nonclickablePadding);
            productToggles[i].SetColors(checkedColor, uncheckedColor, highlightedColor);
        }
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

    private void SubscribeToToggleState()
    {
        toggleObject.GetComponent<Toggle>().onValueChanged.AddListener(SetIsEnabled);
    }

    private void SetIsEnabled(bool value)
    {
        string statusText;
        if (value)
        {
            statusText = "enabled";
        }
        else
        {
            statusText = "disabled";
        }

        Debug.Log(name + " has been " + statusText + ".");
        
        isEnabled = value;

        ToggleVisualState(value);
    }

    private void SetToggleObject(GameObject obj)
    {
        toggleObject = obj;
    }

    public void UpdateUIPosition(Vector3 startPos, Vector3 offset, int lineLimit, int index, float padding)
    {
        rectTransform.SetPositionAndRotation(startPos + new Vector3(offset.x * Mathf.Floor(index / lineLimit), offset.y * (index % lineLimit)), Quaternion.identity);
        rectTransform.GetComponentInChildren<AdjustLabelWidth>().SetWidth(offset.x - padding);
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
}