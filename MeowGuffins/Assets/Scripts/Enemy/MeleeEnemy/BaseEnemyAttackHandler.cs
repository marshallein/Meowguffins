using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttackHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(BoxCollider2D))
        {
            var player = collision.GetComponent<BaseMeow>();
            player.TakeDamage(10f);
        }
    }
}
