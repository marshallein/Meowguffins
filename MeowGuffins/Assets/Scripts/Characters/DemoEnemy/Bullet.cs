using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Bullet : MonoBehaviour
{
    private Animator animator;
    public float damage = 1;
    private float test;
    bool isDeath = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            animator = collision.gameObject.GetComponent<Animator>();
            animator.SetTrigger("take_damage");
            //CMDebug.TextPopupMouse(damage.ToString(),collision.gameObject.transform.position);
            collision.GetComponent<BaseMeow>().TakeDamage(damage);
            if(collision.GetComponentInChildren<BaseMeow>().Health <= 0f)
            {
                animator.SetBool("isDeath2", true);
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
