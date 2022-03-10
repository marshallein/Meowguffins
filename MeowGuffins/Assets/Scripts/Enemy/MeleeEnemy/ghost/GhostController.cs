using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(GhostTimer(7));
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
                    player.TakeDamage(5f);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator GhostTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
    }
}
