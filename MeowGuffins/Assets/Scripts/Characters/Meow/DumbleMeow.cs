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
    public float healAmount = 15f;


    public override void OnCharacterAttack(InputAction.CallbackContext context)
    {
        if (Time.time < nextAttackTime) return;

        var checkDirection = IsFacingRight;

        attack_timmer("attack_set");
        var fireballGO = Instantiate(fireball, attack_point.position, attack_point.rotation);

        var fireballScript = fireballGO.GetComponent<FireBall>();
        if (activeDamageBoostItem)
        {
            fireballScript.BoostDamage(activeDamageBoostItem);
        }

        Rigidbody2D fbRB = fireballGO.GetComponent<Rigidbody2D>();
        if (checkDirection)
        {
            fbRB.velocity = fireballGO.transform.right * fireballForce;
        }
        else
        {
            fireballGO.transform.rotation = new Quaternion(0, -180, 0, 0);
            fbRB.velocity = fireballGO.transform.right * fireballForce;
        }
        nextAttackTime = Time.time + 1f / MeowObject.AttackRate;

    }

    public override void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_timmer("healing");

        health = Mathf.Min(MeowObject.Health, health + healAmount);
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
                IsVulnerable = false;
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
