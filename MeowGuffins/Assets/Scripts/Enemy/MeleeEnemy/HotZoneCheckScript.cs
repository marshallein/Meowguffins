using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheckScript : MonoBehaviour
{
    private EnenyMeleeBehaviour m_parent;
    private bool inRange;
    private Animator animator;
    private GameObject target;


    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        m_parent = GetComponentInParent<EnenyMeleeBehaviour>();
    }

    private void Update()
    {
        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("isAttack"))
        {
            m_parent.OnFlip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject;
        if (target.CompareTag("Player"))
        {
            inRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            m_parent.triggerZone.SetActive(true);
            m_parent.inRange = false;
            m_parent.OnSelectAttack();
        }
    }
}
