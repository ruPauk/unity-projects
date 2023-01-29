using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bulletPrefab;

    private float gravityAtStart;
    private bool isAlive = true;

    Vector2 moveInput;
    Rigidbody2D rigidbody;
    Transform transform;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Transform bowTransform;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        var child = transform.GetChild(0);
        bowTransform = child;
        gravityAtStart = rigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        JumpingState();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnFire(InputValue value)
    {
        if (isAlive)
        {
            var bullet = Instantiate(bulletPrefab, new Vector2(bowTransform.position.x, bowTransform.position.y), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 2f);
            animator.SetBool("IsShooting", true);
            //animator.SetFloat("IsShooting", 10f);
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        var mask = LayerMask.GetMask("Ground");
        if (value.isPressed && feetCollider.IsTouchingLayers(mask))
        {
            rigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void FlipSprite()
    {
        bool hasPlayerHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if (hasPlayerHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), transform.localScale.y);
    }

    void Run()
    {
        if (rigidbody != null)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidbody.velocity.y);
            rigidbody.velocity = playerVelocity;
        }
        var isMoving = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", isMoving);
    }


    /*void ClimbLadder()
    {
        var isLadderInTouch = collider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        var isGroundInTouch = collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        var isMovingVertically = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        Debug.Log(isLadderInTouch + " " + isGroundInTouch + " " + isMovingVertically);
        animator.SetBool("IsSackSmacking", (isLadderInTouch && !isGroundInTouch));

        if (isLadderInTouch)
        {
            rigidbody.gravityScale = 0f;
            Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, moveInput.y * climbSpeed);
            rigidbody.velocity = climbVelocity;

            if (!isMovingVertically && !isGroundInTouch)
            {
                animator.StopPlayback();
            }
            else
            {
                //if (!animator.isActiveAndEnabled)
                //animator.StartPlayback();
            }
                
        }
        else
        {
            rigidbody.gravityScale = gravityAtStart;
        }
    }*/

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rigidbody.gravityScale = gravityAtStart;
            animator.SetBool("IsSackSmacking", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, moveInput.y * climbSpeed);
        rigidbody.velocity = climbVelocity;
        rigidbody.gravityScale = 0f;

        bool hasPlayerVerticalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;

        animator.SetBool("IsSackSmacking", hasPlayerVerticalSpeed);
    }

    void JumpingState()
    {
        var mask = LayerMask.GetMask("Ground");
        animator.SetBool("IsJumping", !feetCollider.IsTouchingLayers(mask));
    }

    

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rigidbody.velocity = deathKick;
        }
    }

   

}
