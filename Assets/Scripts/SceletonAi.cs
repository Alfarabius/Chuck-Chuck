using UnityEngine;
using Pathfinding;
using System;

public class SceletonAi : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Transform currentTarget;
    [SerializeField] private float speed = 400f;
    [SerializeField] private float toJumpValue = 0.4f;
    [SerializeField] private float graphUpdateTime = 1.5f;
    [SerializeField] private float nextWayPointDistance = 1.1f;
    [SerializeField] private Transform enemySprite;
    [SerializeField] private Animator animator;

    private Path path;
    private int currentWayPoint = 0;

    Seeker seeker;
    Rigidbody2D rb;
    CharacterController2D characterController2D;

    private float horizontalMove = 0f;
    private bool isJumping = false;

    void Start()
    {
        characterController2D = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        currentTarget = target;

        InvokeRepeating("UpdatePath", 0f, graphUpdateTime);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && currentTarget != null)
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

    }

    private void Update()
    {
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
            return;

        animator.SetBool("IsRunning", Mathf.Abs(horizontalMove) > 0.01f);

        if (!characterController2D.IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }

        Vector2 rawDirection = (Vector2)path.vectorPath[currentWayPoint] - rb.position;
        Vector2 direction = rawDirection.normalized;
        Vector2 force = speed * direction;

        horizontalMove = force.x;

        if (rawDirection.y >= toJumpValue && characterController2D.IsGrounded())
        {
            isJumping = true;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
            return;

        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, isJumping);
        isJumping = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnNewTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        animator.SetBool("IsAttacking", true);
    }

    public void OnNoNewTarget()
    {
        currentTarget = target;
        animator.SetBool("IsAttacking", false);
    }

    public void SetTarget(Transform _target) => target = _target;
}
