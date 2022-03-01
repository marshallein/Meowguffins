using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public float speed = 2f;
    public Rigidbody2D rb;
    int count = 0;
    private float damage = 20f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" )
        {
            var enemy = collision.GetComponentInChildren<EnemyBehaviour>();
            enemy.TakeDamage(damage);
            
        }
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

    // Update is called once per frame
    void Update()
    {
    }
}
