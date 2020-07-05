using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndFlip : MonoBehaviour
{
    public Vector3 destPos;

    private float moveSpeed = 2f;

    private float flipDist = 0.5f;

    private float flipSpeed = 3f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destPos, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, destPos) < flipDist)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * flipSpeed);
        }
    }
}
