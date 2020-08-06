using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRemoveSound : MonoBehaviour
{
    public void PlayBeepSound()
    {
        AudioManager.Instance.Play("Remove");
    }
}
