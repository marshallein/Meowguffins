using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class BaseAttackHandler : MonoBehaviour
{
    private BaseMeow baseMeow;

    private void Awake()
    {
        baseMeow = GetComponentInParent<BaseMeow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (baseMeow != null)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    var enemy = collision.GetComponentInParent<EnemyHealthController>();
                    enemy.TakeDamage(baseMeow.AttackDamage);
                }

                if (collision.gameObject.tag == "Boss")
                {
                    var boss = collision.GetComponentInParent<BossHeathController>();
                    boss.BossTakeDamage(baseMeow.AttackDamage);
                }
            }
        }
    }


}
