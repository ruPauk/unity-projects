using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runningSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] Vector2 deathSequence = new Vector2(10f, 10f);
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bow;

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    int groundLayerId;
    int ladderLayerId;
    float playerGravity;
    bool isAlive;



    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        groundLayerId = LayerMask.GetMask("Ground");
        ladderLayerId = LayerMask.GetMask("Ladder");
        playerGravity = playerRigidbody.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprites();
        ClimbLadder();
        Die();
    }

    private void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidbody.velocity = deathSequence;
            playerRigidbody.rotation = 60f;
            playerBodyCollider.enabled = false;
            playerFeetCollider.enabled = false;
        }
    }

    private void ClimbLadder()
    {
        if (playerFeetCollider.IsTouchingLayers(ladderLayerId)) // && IsPlayerMovingVertically())
        {
            //playerGravity = playerRigidbody.gravityScale;
            playerRigidbody.gravityScale = 0f;
            Vector2 playerVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * runningSpeed);
            playerRigidbody.velocity = playerVelocity;

            playerAnimator.SetBool("IsBasicClimbing", IsPlayerMovingVertically());
        }
        else
        {
            playerRigidbody.gravityScale = playerGravity;
            playerAnimator.SetBool("IsBasicClimbing", false);
        }
    }

    private void FlipSprites()
    {
       // bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (IsPlayerMovingHorizontally())
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && playerFeetCollider.IsTouchingLayers(groundLayerId))
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runningSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        playerAnimator.SetBool("IsBasicRunning", IsPlayerMovingHorizontally());
        
    }

    bool IsPlayerMovingHorizontally()
    {
        return Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    }

    bool IsPlayerMovingVertically()
    {
        return Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(arrow, bow.transform.position, Quaternion.Euler(0f, 0f, 90f * transform.localScale.x * (-1)));
    }
}
