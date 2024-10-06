using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEffect : MonoBehaviour
{
    public void OnLanding()
    {
        gameObject.SetActive(true);
    }

    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
