using UnityEngine;
using Pathfinding;
using System;

public class SceletonAi : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 400f;

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

        InvokeRepeating("UpdatePath", 0f, 1.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = speed * direction;

        horizontalMove = force.x;

        if (force.y >= 0.01)
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
}
