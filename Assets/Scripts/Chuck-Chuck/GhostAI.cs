using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnNewTarget(Transform newTarget)
    {
        animator.SetBool("IsAttacking", true);
    }

    public void OnNoNewTarget()
    {
        animator.SetBool("IsAttacking", false);
    }
}
