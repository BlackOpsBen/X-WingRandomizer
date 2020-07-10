using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPath : MonoBehaviour
{
    private float minX = -12f;

    private float maxX = 19f;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
    }
}
