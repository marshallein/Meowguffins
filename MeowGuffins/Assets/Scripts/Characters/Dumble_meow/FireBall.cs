using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float baseDamage = 15f;
    private DamageScriptable damageBoost;

    public void BoostDamage(DamageScriptable boost)
    {
        damageBoost = boost;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var finalDamage = baseDamage + (damageBoost == null ? 0 : damageBoost.amount);
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.GetComponentInParent<EnemyHealthController>();
            enemy.TakeDamage(finalDamage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            var boss = collision.GetComponentInParent<BossHeathController>();
            boss.BossTakeDamage(finalDamage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        StartCoroutine(destroyBullet());
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}
