using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rushAttackBehaviour : StateMachineBehaviour
{

    private int m_rand;
    private Transform m_target;
    private BossController m_bossController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_rand = Random.Range(0, 3);
        m_bossController = animator.GetComponent<BossController>();
        m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (m_rand == 0)
        {
            animator.SetTrigger("isSpawnAttack");
            m_bossController.LookAtPlayer(m_target);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
