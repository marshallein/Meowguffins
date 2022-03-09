using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public float speed = 2f;
    public Rigidbody2D rb;
    public bool isSpecial;

    private float baseDamage = 10f;
    private DamageScriptable damageBoost;

    public void BoostDamage(DamageScriptable boost)
    {
        damageBoost = boost;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSpecial)
        {
            baseDamage = 25f;
        }
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
