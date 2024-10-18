using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] List<GameObject> LootDrop;

    void AnimationEnd()
    {
        gameObject.SetActive(false);

        foreach (GameObject gameObject in LootDrop)
        {
            Instantiate(gameObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
