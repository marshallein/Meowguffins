using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float damage = 500f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.GetComponent<BaseMeow>();
            player.IsVulnerable = true;
            player.TakeDamage(damage);
        }
    }
}
