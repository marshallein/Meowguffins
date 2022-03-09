using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    public float speed;
    public float attackRange;
    private Transform m_Player;
    private Rigidbody2D m_Rigidbody;
    private BossController m_BossController;
    private bool m_isAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Rigidbody = animator.GetComponent<Rigidbody2D>();

        m_BossController = animator.GetComponent<BossController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_BossController.LookAtPlayer(m_Player);

        if (!m_isAttack)
        {
            Vector2 target = new Vector2(m_Player.position.x, m_Rigidbody.position.y);
            Vector2 newPos = Vector2.MoveTowards(m_Rigidbody.position, target, speed * Time.fixedDeltaTime);
            m_Rigidbody.MovePosition(newPos);
        }

        if (Vector2.Distance(m_Rigidbody.position, m_Player.position) <= attackRange)
        {
            animator.SetTrigger("isAttack");
            m_isAttack = true;
        }
        else
        {
            m_isAttack = false;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isAttack");
    }
}
