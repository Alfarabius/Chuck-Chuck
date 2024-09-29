using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;

    [SerializeField] private Transform rayPoint;

    [SerializeField] private float rayDistance;

    [SerializeField] private float force = 2f;

    [SerializeField] private GameObject grabbedObject;

    private int layerIndex;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Items");
    }

    void Update()
    {
        if (grabbedObject != null && Input.GetMouseButtonDown(0))
        {
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Dynamic;
            grabbedObject.GetComponent<Collider2D>().enabled = true;
            grabbedObject.transform.SetParent(null);
            rb.AddForce(transform.right * force, ForceMode2D.Impulse);

            grabbedObject = null;
            return;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if(Input.GetMouseButtonDown(0) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;

                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;

                grabbedObject.GetComponent<Collider2D>().enabled = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }
    }
}
