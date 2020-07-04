using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCards : MonoBehaviour
{
    [SerializeField] GameObject pilotCardModel;
    [SerializeField] Transform addonCardParent;
    [SerializeField] GameObject addonCardModel;

    private List<GameObject> addonCardObjects = new List<GameObject>();

    private const float xOffset = 2.25f;
    private const float yOffset = -3.25f;

    public int faceMatIndex = 1;
    public int backMatIndex = 2;

    public void DisplayPilot(Texture texture)
    {
        pilotCardModel.GetComponent<MeshRenderer>().materials[faceMatIndex].mainTexture = texture;
    }

    public void DisplayAddons(List<AddonCard> addonCards)
    {
        ClearPreviousCards();

        int count = addonCards.Count;

        float pX = addonCardParent.position.x;
        float pY = addonCardParent.position.y;

        for (int i = 0; i < count; i++)
        {
            Vector3 relPos = Vector3.zero;

            float newRelX = pX + xOffset * Mathf.Abs(((i + 2) % 2)-1) * CheckForFirstCard(i);
            float newRelY = yOffset * ((i + 2) % 2);

            relPos = new Vector3(newRelX, newRelY, 0f);

            pX = newRelX;
            pY = newRelY;

            GameObject newCardObject = Instantiate(addonCardModel, relPos, Quaternion.identity, addonCardParent);

            addonCardObjects.Add(newCardObject);
        }
    }

    private void ClearPreviousCards()
    {
        foreach (GameObject cardObject in addonCardObjects)
        {
            Destroy(cardObject);
        }
        addonCardObjects.Clear();
    }

    private int CheckForFirstCard(int i)
    {
        if (i == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
