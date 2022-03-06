using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;
    private float baseDamage = 20f;
    private DamageScriptable damageBoost;

    public void BoostDamage(DamageScriptable boost)
    {
        damageBoost = boost;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        var finalDamage = baseDamage + (damageBoost == null ? 0 : damageBoost.amount);
        var enemy = collision.GetComponentInParent<EnemyHealthController>();
        enemy.TakeDamage(finalDamage);
    }

    private void Awake()
    {
        StartCoroutine(destroyBullet());
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
