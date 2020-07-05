using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitView : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();

    //public float minZoom = 75f;
    //public float maxZoom = 60f;

    private Vector3 offset = new Vector3(0f, 0f, -5f);

    private float speed = 2f;

    private float hypotenuse;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("card");

        for (int i = 0; i < cards.Length; i++)
        {
            targets.Add(cards[i].transform);
        }

        if (targets.Count == 0)
        {
            return;
        }

        MoveToFit();
        //ZoomToFit();
    }

    //private void ZoomToFit()
    //{
    //    float newZoom = Mathf.Lerp(maxZoom, minZoom, hypotenuse);
    //    cam.fieldOfView = newZoom;
    //}

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

        hypotenuse = Mathf.Sqrt(Mathf.Pow(bounds.size.x,2f) + Mathf.Pow(bounds.size.y,2));

        return bounds.center;
    }

    public void ClearTargets()
    {
        targets.Clear();
    }
}
