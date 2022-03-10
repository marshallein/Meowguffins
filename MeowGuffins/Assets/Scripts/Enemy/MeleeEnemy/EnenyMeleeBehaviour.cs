using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyMeleeBehaviour : MonoBehaviour
{
    #region Public Variable
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public bool inRange;
    public GameObject hotZone;
    public GameObject triggerZone;
    public float startDazedTime;
    [HideInInspector]
    public float dazedTime;
    public Animator animator;
    #endregion

    #region private variables
    private bool m_attackMode;
    private float m_distance;
    private bool cooling;
    private float m_intTimer;
    private float m_tempSpeed;
    #endregion

    public bool AttackMode { get { return m_attackMode; } set { m_attackMode = value; } }
    public float IntTimer { get { return m_intTimer; } }
    public float Horizontal { get; set; }


    public void Awake()
    {
        m_intTimer = timer;
        animator = GetComponent<Animator>();
        m_tempSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        OnSelectAttack();
    }

    // Update is called once per frame
    public void Update()
    {
        //if (leftLimit == null || rightLimit == null || target == null) return;

        if (dazedTime > 0)
        {
            moveSpeed = 0;
            dazedTime -= Time.deltaTime;
        }
        else
        {
            moveSpeed = m_tempSpeed;
        }

        if (!m_attackMode)
        {
            OnMove();
        }

        if (!InsideOfBound() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("isAttack"))
        {
            OnSelectAttack();
        }



        if (inRange)
        {
            EnemyLogic();
        }

    }

    public void OnSelectAttack()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        OnFlip();
    }

    public void OnFlip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            Horizontal = -1;
            rotation.y = 180f;
        }
        else
        {
            Horizontal = 1;
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }

    private void EnemyLogic()
    {
        m_distance = Vector2.Distance(transform.position, target.position);
        if (m_distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= m_distance && cooling == false)
        {
            OnAttack();
        }

        if (cooling)
        {
            Cooldown();
            animator.SetBool("isAttack", false);
        }
    }

    private void StopAttack()
    {
        cooling = false;
        m_attackMode = false;

        animator.SetBool("isAttack", false);
        animator.SetBool("isWalk", true);
    }

    public virtual void OnAttack()
    {
        timer = m_intTimer;
        m_attackMode = true;

        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", true);
    }

    public virtual void OnMove()
    {
        animator.SetBool("isWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }



    private void TriggerCooling()
    {
        cooling = true;
    }

    private void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && m_attackMode)
        {
            cooling = false;
            timer = m_intTimer;
        }
    }

    private bool InsideOfBound()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

}
