using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetsMask;
    Transform target;
    [SerializeField] private AIPath aIPath;

    private void Start()
    {
        target = GetComponent<AIDestinationSetter>().target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((targetsMask & (1 << layer)) != 0)
        {
            target.gameObject.GetComponent<Destroyable>().TakeDamage(damage, gameObject.name);
            Destroy(gameObject);
        }
    }
}
