using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndFlip : MonoBehaviour
{
    private Vector3 destPos;

    private Quaternion destRot = Quaternion.Euler(Vector3.zero);

    private float moveSpeed = 2f;

    private float flipDist = 0.5f;

    private float flipSpeed = 3f;

    private bool goingUp = false;

    private bool flipSoundPlayed = false;

    private float showBadgeDist = 0.1f;

    private bool destPosSet = false;
    public bool hasArrived { get; private set; }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destPos, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, destPos) < flipDist || goingUp)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, destRot, Time.deltaTime * flipSpeed);
            if (!flipSoundPlayed)
            {
                AudioManager.Instance.Play("FlipAddon");
                flipSoundPlayed = true;
            }
        }

        if (destPosSet && Vector3.Distance(transform.position, destPos) < showBadgeDist)
        {
            hasArrived = true;
        }
    }

    public void SetDestPos(Vector3 newDestPos)
    {
        destPos = newDestPos;
        destPosSet = true;
    }

    public void ExitCard()
    {
        flipSoundPlayed = false;
        gameObject.tag = "Untagged";
        goingUp = true;
        destRot = Quaternion.Euler(new Vector3(0f, -180f, 0f));
        destPos = transform.position + Vector3.up * 10;
    }
}
