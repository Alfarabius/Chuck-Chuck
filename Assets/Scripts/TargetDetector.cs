using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private LayerMask targetsMask;
    [SerializeField] private UnityEvent<Transform> OnNewTarget;
    [SerializeField] private UnityEvent OnNewTargetExit;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRate = 2f;
    float nextAttackTime = 0f;

    private Transform newTarget;

    private void Update()
    {
        if (newTarget != null && Time.time >= nextAttackTime)
        {
            Destroyable destroyable = newTarget.gameObject.GetComponent<Destroyable>();

            if (destroyable)
                destroyable.TakeDamage(damage, transform.parent.name);

            nextAttackTime = Time.time + 1f / attackRate;
        }

        foreach (GameObject target in targets)
        {
            if (target.CompareTag("Player"))
            {
                newTarget = target.transform;
            }
            else
            {
                newTarget = targets[0].transform;
            }
            OnNewTarget.Invoke(newTarget);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((targetsMask & (1 << layer)) != 0)
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((targetsMask & (1 << layer)) != 0)
        {
            targets.Remove(other.gameObject);

            if (targets.Count == 0)
            {
                newTarget = null;
                OnNewTargetExit.Invoke();
            }
        }
    }

}
