using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCards : MonoBehaviour
{
    [SerializeField] GameObject pilotCardModel;
    [SerializeField] Transform addonCardParent;
    [SerializeField] GameObject addonCardModel;

    [SerializeField] FitView fitView;
    [SerializeField] private CreateRemoveBadges createRemoveBadges;

    private List<GameObject> addonCardObjects = new List<GameObject>();

    private const float xOffset = 2.25f;
    private const float yOffset = -3.25f;

    public int faceMatIndex = 1;
    public int backMatIndex = 2;

    public void DisplayPilot(Texture texture)
    {
        pilotCardModel.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        pilotCardModel.GetComponent<MeshRenderer>().materials[faceMatIndex].mainTexture = texture;

        pilotCardModel.GetComponent<FlipPilotCard>().SetDestRot(Vector3.zero);
    }

    public void DisplayAddons(List<AddonCard> addonCards)
    {
        ClearPreviousCards();

        AddonCard[] addonCardsArray = addonCards.ToArray();

        int count = addonCards.Count;

        float pX = addonCardParent.position.x;
        float pY = addonCardParent.position.y;

        for (int i = 0; i < count; i++)
        {
            Vector3 zOffset = new Vector3(0f, 0f, 0.5f);

            Vector3 relPos = Vector3.zero;

            float newRelX;
            float newRelY;

            if (count > 3)
            {
                newRelX = pX + xOffset * Mathf.Abs(((i + 2) % 2) - 1) * CheckForFirstCard(i);
                newRelY = (yOffset * ((i + 2) % 2)) - yOffset/2;
            }
            else
            {
                newRelX = pX + xOffset * CheckForFirstCard(i);
                newRelY = 0f;
            }

            relPos = new Vector3(newRelX, newRelY, 0f);

            pX = newRelX;
            pY = newRelY;

            GameObject newCardObject = Instantiate(addonCardModel, Vector3.zero + (zOffset * (i + 1)), Quaternion.Euler(0f, 180f, 0f), addonCardParent);

            addonCardObjects.Add(newCardObject);

            newCardObject.GetComponent<MeshRenderer>().materials[faceMatIndex].mainTexture = addonCardsArray[i].cardArt.texture;

            MoveAndFlip moveAndFlip = newCardObject.AddComponent<MoveAndFlip>();

            moveAndFlip.SetDestPos(relPos);

            createRemoveBadges.CreateNewBadge(newCardObject);
        }
    }

    public void ClearPreviousCards()
    {
        fitView.ClearTargets();
        foreach (GameObject cardObject in addonCardObjects)
        {
            cardObject.GetComponent<MoveAndFlip>().ExitCard();
            Destroy(cardObject, 1f);
        }
        addonCardObjects.Clear();
        createRemoveBadges.ResetBadges();
    }

    public void ClearSingleCard(int index)
    {
        fitView.ClearSingleTarget(index);
        GameObject singleCardObject = addonCardObjects[index];
        singleCardObject.GetComponent<MoveAndFlip>().ExitCard();
        addonCardObjects.RemoveAt(index);
        Destroy(singleCardObject, 1f);
    }

    public void FlipPilotCard()
    {
        pilotCardModel.GetComponent<FlipPilotCard>().SetDestRot(new Vector3(0f, -180f, 0f));
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
