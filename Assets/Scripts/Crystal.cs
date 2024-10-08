using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private LayerMask targets;
    [SerializeField] private Collider2D triggerZone;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float cooldownSeconds = 1f;
    float cooldown = 0f;

    private Animator animator;
    [SerializeField] private List<Collider2D> guests;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("IsActive", guests.Count > 0);

        if (guests.Count > 0 && Time.time > cooldown)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.GetComponent<AIDestinationSetter>().target = guests[0].transform;

            cooldown = Time.time + cooldownSeconds;
        }
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

    public void Die()
    {
        Destroy(gameObject);
    }
}
