using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RestrictionManager : MonoBehaviour
{
    public static RestrictionManager Instance {get; private set;}

    public ListManager shipRestrictions;

    private void Awake()
    {
        SingletonPattern();
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
