using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitView : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();

    public float minFOV = 60f;
    public float maxFOV = 70f;

    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -5f);

    private float speed = 2f;

    private float zoomSpeed = 2f;

    private Camera cam;

    private GameObject[] cards;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        cards = GameObject.FindGameObjectsWithTag("card");

        targets.Clear();

        for (int i = 0; i < cards.Length; i++)
        {
            targets.Add(cards[i].transform);
        }

        if (targets.Count == 0)
        {
            return;
        }

        MoveToFit();
        ZoomToFit();
    }

    private void ZoomToFit()
    {
        float newFOV;

        if (cards.Length > 6)
        {
            newFOV = maxFOV;
        }
        else
        {
            newFOV = minFOV;
        }
        
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newFOV, Time.deltaTime * zoomSpeed);
    }

    private void MoveToFit()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    public void ClearTargets()
    {
        targets.Clear();
    }

    public void ClearSingleTarget(int index)
    {
        targets.RemoveAt(index);
    }
}
