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

    [SerializeField] CharacterController2D controller2D;

    private int layerIndex;

    private Vector3 cursorPosition;

    private Vector3 side;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Items");
        Cursor.lockState = CursorLockMode.None;
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

        //cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        cursorPosition.z = 0f;

        if (controller2D.IsFacingRight())
        {
            side = Vector3.right;
        }
        else if (!controller2D.IsFacingRight())
        {
            side = Vector3.left;
        }

        cursorPosition.x *= side.x;

        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, cursorPosition.normalized, rayDistance);
        Debug.DrawRay(rayPoint.position, cursorPosition.normalized * rayDistance);

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
