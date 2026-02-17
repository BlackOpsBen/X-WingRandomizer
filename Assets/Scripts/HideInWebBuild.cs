using UnityEngine;

public class HideInWebBuild : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }
}
