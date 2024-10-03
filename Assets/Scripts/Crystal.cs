using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private LayerMask targets;
    [SerializeField] private Collider2D triggerZone;

    private Animator animator;
    [SerializeField] private List<Collider2D> guests;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("IsActive", guests.Count > 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((targets & (1 << layer)) != 0)
        {
            guests.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((targets & (1 << layer)) != 0)
        {
            guests.Remove(other);
        }
    }
}
