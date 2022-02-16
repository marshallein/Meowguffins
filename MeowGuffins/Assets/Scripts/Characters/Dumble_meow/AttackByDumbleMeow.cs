using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackByDumbleMeow : MonoBehaviour
{
    public Transform attack_point;
    public GameObject fireball;
    private Animator animator;
    private Rigidbody2D rb;
    public float fireballForce = 12f;
    private float attackRate = 2f;
    private float attackTime = 0f;
    private bool checkDirection;
    // Start is called before the first frame update
  
    private void AttackTimer(string trigger)
    {
        if (Time.time >= attackTime)
        {
            animator.SetTrigger(trigger);
            attackTime = Time.time + 1f / attackRate;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void onCharacterAttack3(InputAction.CallbackContext context)
    {
        AttackTimer("attack_set3");
    }
    public void onCharacterAttack2(InputAction.CallbackContext context)
    {
        AttackTimer("attack_set2");
    }
    public void onCharacterHealing(InputAction.CallbackContext context)
    {
        AttackTimer("healing");
    }
    public void onCharacterAttack1(InputAction.CallbackContext context)
    {
        if (Time.time >= attackTime)
        {
            AttackTimer("attack_set");
            GameObject fireballSpawn = Instantiate(fireball, attack_point.position, attack_point.rotation);
            Rigidbody2D fbRB = fireballSpawn.GetComponent<Rigidbody2D>();
            if (checkDirection)
            {
                fbRB.velocity = fireballSpawn.transform.right * fireballForce;
            }
            else
            {
                fbRB.velocity = fireballSpawn.transform.right * -fireballForce;
            }
            attackTime = Time.time + 1f / attackRate;
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        checkDirection = ImproveMoveController.m_isFacingRight;

    }
}
