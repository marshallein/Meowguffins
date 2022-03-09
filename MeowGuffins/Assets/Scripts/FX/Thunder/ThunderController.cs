using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{

    public void OnAnimationIsDone()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.GetType() == typeof(BoxCollider2D))
                {
                    var player = collision.GetComponent<BaseMeow>();
                    player.TakeDamage(10f);
                }

            }

        }
    }

}
