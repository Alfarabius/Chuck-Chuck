using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileBased : MonoBehaviour
{
    private Vector3 pointToMove;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask obstacle;

    private void Awake()
    {
        pointToMove = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointToMove, moveSpeed * Time.deltaTime);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, pointToMove) > .05f)
        {
            return;
        }

        if (Mathf.Abs(horizontalInput) == 1f)
        {
            if (!Physics2D.OverlapCircle(pointToMove + new Vector3(horizontalInput, 0f, 0f), .2f, obstacle))
            {
                pointToMove += new Vector3(horizontalInput, 0f, 0f);
            }
        }
        else if (Mathf.Abs(verticalInput) == 1f)
        {
            if (!Physics2D.OverlapCircle(pointToMove + new Vector3(0f, verticalInput, 0f), .2f, obstacle))
            {
                pointToMove += new Vector3(0f, verticalInput, 0f);
            }
        }
    }
}
