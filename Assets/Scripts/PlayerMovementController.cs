using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] UnityEvent onJump;

    private float horizontalMove = 0f;
    private bool isJumping = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }

        if (isJumping && controller.IsGrounded())
            onJump.Invoke();

        if (!controller.IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }

        animator.SetBool("IsRunning", Mathf.Abs(horizontalMove) > 0.01f);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isJumping);
        isJumping = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}
