using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testRandomCards : MonoBehaviour
{
    [SerializeField] Image pilot;
    [SerializeField] Image addon;
    // Start is called before the first frame update
    void Start()
    {
        PilotCard pilotCard = CardManager.Instance.pilots[Random.Range(0, CardManager.Instance.pilots.Length)];
        pilot.sprite = pilotCard.cardArt;
        Missile missile = CardManager.Instance.missiles[Random.Range(0, CardManager.Instance.missiles.Length)];
        addon.sprite = missile.cardArt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
