using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLabelWidth : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetWidth(float xOffset)
    {
        rectTransform.offsetMax = new Vector2(xOffset - rectTransform.offsetMin.x, rectTransform.offsetMax.y);
    }
}
