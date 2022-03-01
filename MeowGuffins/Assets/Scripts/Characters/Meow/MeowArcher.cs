using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeowArcher : BaseMeow
{
    public Transform attack_point;
    public GameObject arrow;
    public GameObject electricArrow;
    public float arrowForce = 15f;

    public override void OnCharacterAttack2(InputAction.CallbackContext context)
    {
        attack_arrow_timmer("attack_set2");
    }

    public override void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_arrow_timmer("attack_set3");
    }

    private void attack_arrow_timmer(string trigger)
    {
        if (Time.time >= nextAttackTime)
        {
            var checkDirection = IsFacingRight;
            animator.SetTrigger(trigger);
            if (trigger == "attack_set2")
            {
                GameObject arrowSpawn = Instantiate(arrow, attack_point.position, attack_point.rotation);
                Rigidbody2D arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
                if (checkDirection)
                {
                    arrowRb.velocity = arrowSpawn.transform.right * arrowForce;
                }
                else
                {
                    arrowSpawn.transform.rotation = new Quaternion(0, -180, 0, 0);
                    arrowRb.velocity = arrowSpawn.transform.right * arrowForce;
                }
            }
            else
            {
                GameObject arrowSpawn = Instantiate(electricArrow, attack_point.position, attack_point.rotation);
                Rigidbody2D arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
                if (checkDirection)
                {
                    arrowRb.velocity = arrowSpawn.transform.right * arrowForce;
                }
                else
                {
                    arrowSpawn.transform.rotation = new Quaternion(0, -180, 0, 0);
                    arrowRb.velocity = arrowSpawn.transform.right * arrowForce;
                }
            }
            nextAttackTime = Time.time + 1f / MeowObject.AttackRate;
        }
    }
}
