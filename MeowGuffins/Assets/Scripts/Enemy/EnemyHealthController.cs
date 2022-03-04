using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public float maxHeath;


    [SerializeField]
    private float currentHeath;
    private EnenyMeleeBehaviour m_enemyMeleeBehaviour;
    private Animator m_animator;

    private void Awake()
    {
        currentHeath = maxHeath;
        m_enemyMeleeBehaviour = GetComponent<EnenyMeleeBehaviour>();
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        currentHeath = maxHeath;
        m_animator.SetBool("isDead", false);

    }

    public void OnDead()
    {
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(float inputDamage)
    {
        m_enemyMeleeBehaviour.dazedTime = m_enemyMeleeBehaviour.startDazedTime;
        currentHeath -= inputDamage;
        if (currentHeath <= 0)
        {
            m_animator.SetTrigger("isDead");
            this.enabled = false;
        }
    }

}
