using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTexture : MonoBehaviour
{
    [SerializeField] Texture desiredTexture;
    private void Awake()
    {
        GetComponent<MeshRenderer>().materials[1].mainTexture = desiredTexture;
    }
}
