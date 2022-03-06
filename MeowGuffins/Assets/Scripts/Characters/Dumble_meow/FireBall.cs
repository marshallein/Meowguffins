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
        if (collision.gameObject.tag != "Enemy") return;

        var finalDamage = baseDamage + (damageBoost == null ? 0 : damageBoost.amount);
        print("damage of fireball " + finalDamage);
        var enemy = collision.GetComponentInChildren<EnemyBehaviour>();
        enemy.TakeDamage(finalDamage);

    }

    private void Awake()
    {
        StartCoroutine(destroyBullet());
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
