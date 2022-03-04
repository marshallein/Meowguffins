using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DumbleMeow : BaseMeow
{
    public Transform attack_point;
    public GameObject fireball;
    public float fireballForce = 12f;
    private float m_LastDodge = -100f;
    public float horizontal_axis;

    public override void OnCharacterAttack(InputAction.CallbackContext context)
    {
        fireball_timmer("attack_set");
    }

    private void fireball_timmer(string trigger)
    {
        if(Time.time >= nextAttackTime)
        {
            var checkDirection = IsFacingRight;
            animator.SetTrigger(trigger);
            GameObject fireballSpawn = Instantiate(fireball, attack_point.position, attack_point.rotation);
            Rigidbody2D fbRB = fireballSpawn.GetComponent<Rigidbody2D>();
            if (checkDirection)
            {
                fbRB.velocity = fireballSpawn.transform.right * fireballForce;
            }
            else
            {
                fireballSpawn.transform.rotation = new Quaternion(0, -180, 0, 0);
                fbRB.velocity = fireballSpawn.transform.right * fireballForce;
            }
            nextAttackTime = Time.time + 1f / MeowObject.AttackRate;
        }
    }

    public override void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_timmer("healing");

        if (health <= MaxHealth - 15f)
        {
            health += 15f / 3;
        }
        else
        {
            health = MaxHealth;
        }
    }

    public override void OnCharacterAttack4(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set3");
    }

    public override void OnCharacterDodge(InputAction.CallbackContext context)
    {
        if (Time.time >= (m_LastDodge + moveController.meow.MeowObject.DodgeCooldown))
        {
            if (!IsAbleToTeleport())
            {
                moveController.m_dodgeTimeLeft = moveController.meow.MeowObject.DodgeTime;
                m_LastDodge = Time.time;
                if (moveController.m_dodgeTimeLeft > 0)
                {
                    TeleportCharacter();
                }
            }
        }
    }

    private bool IsAbleToTeleport()
    {
        if (IsFacingRight)
        {
            horizontal_axis = 1;
        }
        else
        {
            horizontal_axis = -1;
        }
        return Physics2D.Raycast(frontCheck.position, new Vector2(horizontal_axis, 0), moveController.meow.MeowObject.DodgeDistance + 0.2f, moveController.meow.groundCheckLayer);
    }

    private void TeleportCharacter()
    {
        animator.SetTrigger("isDodge");
        transform.position = new Vector3(transform.position.x + (horizontal_axis * moveController.meow.MeowObject.DodgeDistance), transform.position.y);
    }


}
