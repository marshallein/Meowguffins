using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShortTeleScript : MonoBehaviour
{
    private Animator m_Animator;
    private float m_horizontal;
    private RaycastHit2D m_RaycastHit2;
    private bool isNearWall;
    private float m_dodgeTimeLeft;
    private float m_LastDodge = -100f;

    public Transform frontCheck;
    public LayerMask groundMask;

    [Header("Dodge")]
    public float dodgeTime;
    public float teleportDistance = 3f;
    public float dodgeCooldown = 4f;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (checkFacing())
        {
            m_horizontal = 1;
            Debug.DrawRay(new Vector2(frontCheck.position.x, frontCheck.position.y + 0.1f), new Vector2(teleportDistance + 0.2f, 0), Color.red);
        }
        else
        {
            m_horizontal = -1;
            Debug.DrawRay(new Vector2(frontCheck.position.x, frontCheck.position.y + 0.1f), -new Vector2(teleportDistance + 0.2f, 0), Color.red);

        }

        isNearWall = isAbleToTelePort();
    }

    private bool isAbleToTelePort()
    {
        m_RaycastHit2 = Physics2D.Raycast(frontCheck.position, new Vector2(m_horizontal, 0), teleportDistance + 0.2f, groundMask);
        return m_RaycastHit2;
    }

    private bool checkFacing()
    {
        return ImproveMoveController.m_isFacingRight;
    }

    private void TeleportCharacter()
    {
        m_Animator.SetTrigger("isDodge");

        transform.position = new Vector3(transform.position.x + (m_horizontal * teleportDistance), transform.position.y);
    }

    public void OnCharacterTeleport(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time >= (m_LastDodge + dodgeCooldown))
            {
                if (!isAbleToTelePort())
                {
                    m_dodgeTimeLeft = dodgeTime;
                    m_LastDodge = Time.time;
                    if (m_dodgeTimeLeft > 0)
                    {
                        TeleportCharacter();
                    }
                }
            }
        }
    }
}
