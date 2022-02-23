using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float damage = 15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponentInChildren<HealthBar>().hp -= damage;
            if (collision.GetComponentInChildren<HealthBar>().hp <= 0f)
            {
                collision.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
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
