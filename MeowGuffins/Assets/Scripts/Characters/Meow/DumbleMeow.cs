using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DumbleMeow : BaseMeow
{
    public Transform attack_point;
    public GameObject fireball;
    public float fireballForce = 12f;

    public override void OnCharacterAttack(InputAction.CallbackContext context)
    {
        var checkDirection = IsFacingRight;
        if (Time.time >= nextAttackTime)
        {
            attack_timmer("attack_set");
            GameObject fireballSpawn = Instantiate(fireball, attack_point.position, attack_point.rotation);
            Rigidbody2D fbRB = fireballSpawn.GetComponent<Rigidbody2D>();
            if (checkDirection)
            {
                fbRB.velocity = fireballSpawn.transform.right * fireballForce;
            }
            else
            {
                fbRB.velocity = fireballSpawn.transform.right * -fireballForce;
            }
            nextAttackTime = Time.time + 1f / nextAttackTime;
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
}
