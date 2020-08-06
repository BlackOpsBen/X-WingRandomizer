using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldToScreenPos : MonoBehaviour
{
    [SerializeField] private Transform worldTransform;

    private RectTransform rectTransform;

    private Vector3 screenPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (worldTransform != null)
        {
            screenPos = Camera.main.WorldToScreenPoint(worldTransform.position);

            rectTransform.position = screenPos;
        }
    }

    public void SetWorldTransform(Transform transform)
    {
        worldTransform = transform;
    }
}
