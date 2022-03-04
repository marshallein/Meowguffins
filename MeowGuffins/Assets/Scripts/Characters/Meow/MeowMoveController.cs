using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeowMoveController
{
    public BaseMeow meow;

    #region Private variables
    [SerializeField]
    private Rigidbody2D rigidbody;
    private float horizontal;
    private Animator animator;
    private bool m_isDodge;
    private bool m_isWallSliding = false;
    private float m_LastDodge = -100f;
    private ParticleSystem particleSystem;
    #endregion

    public bool m_isFacingRight = true;
    public float m_dodgeTimeLeft;
    //public float move_speed;
    //public float jump_force;
    
    //public float wallJumpTime = 0.02f;
    //public float wallSlideSpeed = 0.06f;
    //public float wallDistance = 0.25f;
    
    //[Header("Dodge")]
    //public float dodgeTime;
    //public float dodgeDistance = 15f;
    //public float dodgeCooldown;

    RaycastHit2D wallCheckHit;
    float jumpTime;

    private Transform transform;


    public MeowMoveController(BaseMeow meow)
    {
        this.meow = meow;
        this.rigidbody = meow.GetComponent<Rigidbody2D>();
        this.animator = meow.GetComponent<Animator>();
        this.transform = meow.transform;
        particleSystem = meow.GetComponentInChildren<ParticleSystem>();
    }

    public void Update()
    {
        animator.SetBool("isGrounded", IsCharacterisGrounded());
        if (m_isWallSliding)
        {
            animator.SetBool("isGrounded", false);
        }
    }

    public void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontal * meow.MeowObject.MoveSpeed, rigidbody.velocity.y);
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


        var wallDistance = meow.MeowObject.WallDistance;
        if (m_isFacingRight)
        {
            wallCheckHit = Physics2D.Raycast(meow.frontCheck.position, new Vector2(wallDistance, 0), wallDistance, meow.groundCheckLayer);
            Debug.DrawRay(meow.frontCheck.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else
        {
            wallCheckHit = Physics2D.Raycast(meow.frontCheck.position, new Vector2(-wallDistance, 0), wallDistance, meow.groundCheckLayer);
            Debug.DrawRay(meow.frontCheck.position, new Vector2(-wallDistance, 0), Color.blue);

        }

        if (wallCheckHit && !IsCharacterisGrounded() && rigidbody.velocity.y <= 0)
        {
            m_isWallSliding = true;
            jumpTime = Time.time + meow.MeowObject.WallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            m_isWallSliding = false;
        }

        if (m_isWallSliding)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, meow.MeowObject.WallSlideSpeed, float.MaxValue));
        }

        if (m_isDodge)
        {

            if (m_dodgeTimeLeft > 0)
            {

                rigidbody.velocity = new Vector2(meow.MeowObject.DodgeDistance * horizontal, rigidbody.velocity.y);
                m_dodgeTimeLeft -= Time.deltaTime;
            }


            if (m_dodgeTimeLeft <= 0 || wallCheckHit != false)
            {
                m_isDodge = false;
            }
        }

    }

    private bool IsCharacterisGrounded()
    {
        return Physics2D.OverlapCircle(meow.groundCheck.position, 0.15f, meow.groundCheckLayer);

    }

    private void OnCharacterFlip()
    {
        CreateDust();
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
            CreateDust();
            var jump_force = meow.MeowObject.JumpForce;
            if (m_isWallSliding && horizontal != 0)
            {
                rigidbody.AddForce(new Vector2(1500 * -horizontal, 0));
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_force);
            }
            else if (IsCharacterisGrounded())
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_force);
            }
        }
        if (context.canceled && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }
    }

    public void OnCharacterDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time >= (m_LastDodge + meow.MeowObject.DodgeCooldown))
            {
                DodgeAction();
            }

        }
    }

    private void DodgeAction()
    {
        m_isDodge = true;
        m_dodgeTimeLeft = meow.MeowObject.DodgeTime;
        m_LastDodge = Time.time;
        animator.SetTrigger("isDodge");
        CreateDust();
    }

    private void CreateDust()
    {
        particleSystem.Play();
    }
}
