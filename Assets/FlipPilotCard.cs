using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPilotCard : MonoBehaviour
{
    private Vector3 destRot = new Vector3(0f, -180f, 0f);

    private float flipSpeed = 2f;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(destRot), Time.deltaTime * flipSpeed);
    }

    public void SetDestRot(Vector3 newDestRot)
    {
        destRot = newDestRot;
    }
}
