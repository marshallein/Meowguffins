using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackDemoScript : MonoBehaviour
{

    private Animator animator;
    Rigidbody2D rb;
    float nextAttackTime = 0f;
    float attackRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void attack_timmer(string trigger)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger(trigger);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void OnCharacterAttack(InputAction.CallbackContext context) {
        attack_timmer("attack_set");
    }

    public void OnCharacterAttack2(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set2");
    }

    public void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set3");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hit " + collision.name);
    }
}
