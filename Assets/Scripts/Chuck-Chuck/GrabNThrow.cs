using System;
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
    private Vector3 cursorPosition;
    GameObject oldTarget;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Items");
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.x = -rayPoint.position.x + cursorPosition.x;
        cursorPosition.y = -rayPoint.position.y + cursorPosition.y;
        cursorPosition.z = 0f;

        if (grabbedObject != null && Input.GetMouseButtonDown(0)) // Throw
        {
            CheckFacing(cursorPosition);

            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Dynamic;
            grabbedObject.GetComponent<Collider2D>().enabled = true;
            grabbedObject.transform.SetParent(null);
            rb.AddForce(cursorPosition.normalized * force, ForceMode2D.Impulse);

            grabbedObject = null;
            return;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, cursorPosition.normalized, rayDistance);
        Debug.DrawRay(rayPoint.position, cursorPosition.normalized * rayDistance);

        if (hitInfo.collider == null)
            return;

        GameObject target = hitInfo.collider.gameObject;
        bool isOldTargetDamageDealer = false;

        bool isTargetDamageDealer = target.TryGetComponent<DamageDealer>(out DamageDealer targetDamageDealer);
        if (oldTarget != null)
            isOldTargetDamageDealer = oldTarget.TryGetComponent<DamageDealer>(out DamageDealer oldTargetDamageDealer);

        if (isTargetDamageDealer)
        {
            if (target != oldTarget && grabbedObject == null)
            {
                if (oldTarget != null && isOldTargetDamageDealer)
                    oldTarget.GetComponent<DamageDealer>().Unselect();
                targetDamageDealer.Select();
            }
        }
        else
        {
            if (oldTarget != null && isOldTargetDamageDealer)
                oldTarget.GetComponent<DamageDealer>().Unselect();
        }

        oldTarget = target;

        if (hitInfo.collider != null && target.layer == layerIndex) // Grab
        {
            if(Input.GetMouseButtonDown(0) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<DamageDealer>().Unselect();

                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;

                grabbedObject.GetComponent<Collider2D>().enabled = false;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }
    }

    private void CheckFacing(Vector3 cursorPosition)
    {
        PlayerMovement characterController = GetComponent<PlayerMovement>();

        if (cursorPosition.x < -0.1f && characterController.IsFacingRight)
        {
            characterController.Turn();
        }
        else if (cursorPosition.x > 0.1f && !characterController.IsFacingRight)
        {
            characterController.Turn();
        }
    }
}
