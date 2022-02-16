using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackDemoMeowArcher : MonoBehaviour
{
    //private Transform arrowSpawn;
    public Transform attack_point;
    public GameObject arrow;
    public GameObject electricArrow;
    private Animator animator;
    Rigidbody2D rb;
    private bool checkDirection;
    float nextAttackTime = 0f;
    float attackRate = 2f;
    public float arrowForce = 15f;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void attack_timmer(string trigger)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger(trigger);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void attack_arrow_timmer(string trigger)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger(trigger);
            if (trigger == "attack_set2")
            {
                GameObject arrowSpawn = Instantiate(arrow, attack_point.position, attack_point.rotation);
                Rigidbody2D arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
                if (checkDirection)
                {
                    arrowRb.velocity = arrowSpawn.transform.right * 10;
                }
                else
                {
                    arrowRb.velocity = arrowSpawn.transform.right * -10;
                }
            }
            else
            {
                GameObject arrowSpawn = Instantiate(electricArrow, attack_point.position, attack_point.rotation);
                Rigidbody2D arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
                if (checkDirection)
                {
                    arrowRb.velocity = arrowSpawn.transform.right * 10;
                }
                else
                {
                    arrowRb.velocity = arrowSpawn.transform.right * -10;
                }
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void OnCharacterAttack(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set");
    }

    public void OnCharacterAttackArrow(InputAction.CallbackContext context)
    {
        attack_arrow_timmer("attack_set2");
    }

    public void OnCharacterAttackElectricArrow(InputAction.CallbackContext context)
    {
        attack_arrow_timmer("attack_set3");
    }

    public void OnCharacterJumpingAttack(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set4");
    }

    // Update is called once per frame
    void Update()
    {
        checkDirection = ImproveMoveController.m_isFacingRight;
    }
}
