using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaction : MonoBehaviour
{
    [SerializeField] private int faceMatIndex = 1;
    [SerializeField] private int backMatIndex = 2;

    [SerializeField] private Texture[] cardTextures;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeCardBack(int selectedFaction)
    {
        RotateAndTexture(cardTextures[selectedFaction]);
    }

    private void RotateAndTexture(Texture texture)
    {
        meshRenderer.materials[faceMatIndex].mainTexture = meshRenderer.materials[backMatIndex].mainTexture;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        meshRenderer.materials[backMatIndex].mainTexture = texture;
        AudioManager.Instance.Play("FlipPilot");
    }
}
