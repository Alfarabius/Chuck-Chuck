using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PackageGD : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            transform.parent.GetComponent<Destroyable>().TakeDamage(1, "Ground");
        }
    }
}
