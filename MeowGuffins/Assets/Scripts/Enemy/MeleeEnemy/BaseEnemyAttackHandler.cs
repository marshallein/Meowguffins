using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttackHandler : MonoBehaviour
{

    public float damage = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetType() == typeof(BoxCollider2D))
            {
                var player = collision.GetComponent<BaseMeow>();
                player.TakeDamage(damage);
            }
        }
    }
}
