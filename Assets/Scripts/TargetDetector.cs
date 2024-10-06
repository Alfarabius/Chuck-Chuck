using System.Collections;
using System.Collections.Generic;
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
                destroyable.TakeDamage(damage);

            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (targets.Count == 0 && newTarget != null)
        {
            newTarget = null;
            OnNewTargetExit.Invoke();
        }

        if (targets.Count > 0 && newTarget != null)
        {
            if (newTarget.CompareTag("Player"))
                return;

            foreach(GameObject t in targets)
            {
                if (t.CompareTag("Player"))
                {
                    newTarget = t.transform;
                    OnNewTarget.Invoke(newTarget);
                    return;
                }
            }

            return;
        }

        if (targets.Count > 0 && newTarget == null)
        {
            foreach(GameObject t in targets)
            {
                if (t.CompareTag("Player"))
                {
                    newTarget = t.transform;
                    OnNewTarget.Invoke(newTarget);
                    return;
                }
            }

            newTarget = targets[0].transform;
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
        }
    }

}
