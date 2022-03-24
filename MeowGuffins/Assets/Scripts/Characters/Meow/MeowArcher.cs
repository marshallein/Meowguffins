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
        attack_arrow_timmer("attack_set2", false, arrow);
    }

    public override void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_arrow_timmer("attack_set3", true, electricArrow);
    }

    private void attack_arrow_timmer(string trigger, bool isSpecialArrow, GameObject arrowType)
    {
        if (!IsVulnerable) return;
        if (Time.time < nextAttackTime) return;

        var checkDirection = IsFacingRight;

        animator.SetTrigger(trigger);
        var arrowSpawn = Instantiate(arrowType, attack_point.position, attack_point.rotation);

        var arrowScript = arrowSpawn.GetComponent<ArrowScript>();
        if (activeDamageBoostItem)
        {
            arrowScript.BoostDamage(activeDamageBoostItem);
        }

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

        if (isSpecialArrow)
        {
            nextAttackTime = Time.time + 7f / meowObject.AttackRate;
        }
        else
        {
            nextAttackTime = Time.time + 1f / MeowObject.AttackRate;
        }

    }
}
