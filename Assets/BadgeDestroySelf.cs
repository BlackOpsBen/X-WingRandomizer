using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeDestroySelf : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
