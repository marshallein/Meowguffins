using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImproveMoveController : MonoBehaviour
{

    #region Private variables
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Transform m_groundCheck;
    private bool m_isFacingRight = true;
    private float horizontal;
    private Animator animator;
    private bool m_isWallSliding = false;
    #endregion

    public LayerMask groundCheckLayer;
    public float move_speed;
    public float jump_force;

    public float wallJumpTime = 0.02f;
    public float wallSlideSpeed = 0.06f;
    public float wallDistance = 0.27f;

    RaycastHit2D wallCheckHit;
    float jumpTime;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        animator.SetBool("isGrounded", OnCharacterisGrounded());
        if (m_isWallSliding)
        {
            animator.SetBool("isGrounded", false);
        }
    }

    private void FixedUpdate()
    {

        rigidbody.velocity = new Vector2(horizontal * move_speed, rigidbody.velocity.y);
        if (horizontal != 0)
        {
            animator.SetFloat("Speed", 10);
        }
        else if (horizontal == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        if (!m_isFacingRight && horizontal > 0)
        {
            OnCharacterFlip();
        }
        else if (m_isFacingRight && horizontal < 0)
        {
            OnCharacterFlip();
        }


        if (m_isFacingRight)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundCheckLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundCheckLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);

        }

        if (wallCheckHit && !OnCharacterisGrounded() && rigidbody.velocity.y <= 0)
        {
            m_isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            m_isWallSliding = false;
        }

        if (m_isWallSliding)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, wallSlideSpeed, float.MaxValue));
        }

    }

    private bool OnCharacterisGrounded()
    {
        return Physics2D.OverlapCircle(m_groundCheck.position, 0.2f, groundCheckLayer);
    }

    private void OnCharacterFlip()
    {
        m_isFacingRight = !m_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void OnReadCharacterMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnCharacterJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_isWallSliding && horizontal != 0)
            {
                rigidbody.AddForce(new Vector2(1500 * -horizontal, 0));
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_force);
            }
            else if(OnCharacterisGrounded())
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_force);
            }
        }
        if (context.canceled && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }
    }
}
