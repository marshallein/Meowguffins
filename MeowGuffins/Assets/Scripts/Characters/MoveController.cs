using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    const float speed = 6f;
    const float jumpForce = 10f;
    private float moveInput;
    
    private bool isGrounded;
    public Transform feetPos;
    const float checkRadius = 0.3f;
    public LayerMask whatIsGround;
    public Animator animator;

    const float jumpTime = 0.35f;
    private float jumpTimeCounter;
    private bool isJumping;

    bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    const float wallSlidingSpeed = 5f;

    bool wallJumping;
    const float xWallForce = 15f;
    const float yWallForce = 15f;
    const float wallJumpTime = 0.05f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
/*    void FixedUpdate()
    {
    }*/

    private void basicLeftRightMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void movementControl()
    {
        if (moveInput > 0)
        {
            animator.SetFloat("Speed", 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            animator.SetFloat("Speed", 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("isJumping", true);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

    }

    private void wallClimbing()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront && !isGrounded)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            float test = Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue);
            if (test == 0f)
            {
                test = -5f;
            }
            rb.velocity = new Vector2(rb.velocity.x, test);
        }
        if (Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    private void Update()
    {
        basicLeftRightMovement();
        wallClimbing();
        movementControl();
    }
    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
