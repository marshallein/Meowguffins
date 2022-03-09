using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushMoveBehaviour : StateMachineBehaviour
{
    private Transform m_target;
    private Rigidbody2D m_Rigidbody;
    private BossController m_Controller;


    public float speed = 4f;
    public float attackRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<BossController>();
        m_Rigidbody = animator.GetComponent<Rigidbody2D>();
        m_target = m_Controller.OnSelectAttack(animator.gameObject.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Controller.InsideOfBound(animator.gameObject.transform))
        {
            m_target = m_Controller.OnSelectAttack(animator.gameObject.transform);
        }
        m_Controller.LookAtPlayer(m_target);

        Vector2 target = new Vector2(m_target.position.x, m_Rigidbody.position.y);
        animator.gameObject.transform.position = Vector2.MoveTowards(m_Rigidbody.position, target, speed * Time.deltaTime);

        m_Controller.CreateThunder(animator.gameObject.transform);

        if (Vector2.Distance(m_target.position, animator.gameObject.transform.position) <= attackRange)
        {
            animator.SetTrigger("isRushAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
