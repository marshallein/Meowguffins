using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIdleBehaviour : StateMachineBehaviour
{

    private Transform m_Player;
    private Rigidbody2D m_Rigidbody;

    public bool isFlipped = false;
    public float speed;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Rigidbody = animator.GetComponent<Rigidbody2D>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer(m_Player);
        Vector2 target = new Vector2(m_Player.position.x, m_Player.position.y);
        Vector2 newPos = Vector2.MoveTowards(m_Rigidbody.position, target, speed * Time.fixedDeltaTime);
        m_Rigidbody.MovePosition(newPos);
    }
    public void LookAtPlayer(Transform player)
    {
        Vector3 flipped = m_Rigidbody.transform.localScale;

        if (m_Rigidbody.transform.position.x > player.position.x && isFlipped)
        {
            m_Rigidbody.transform.localScale = flipped;
            m_Rigidbody.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (m_Rigidbody.transform.position.x < player.position.x && !isFlipped)
        {
            m_Rigidbody.transform.localScale = flipped;
            m_Rigidbody.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
