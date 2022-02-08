using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDemoScript : MonoBehaviour
{

    public Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack_set");
            rb.gravityScale = 20;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rb.gravityScale = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("hit" + collision.name);

    }
}