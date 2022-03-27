using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeathController : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    public GameObject spike;
    public AudioSource takehit;

    private float m_currentHealth;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    public bool isVunerable = false;

    private void Awake()
    {
        healthBar.gameObject.SetActive(true);
        m_currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = m_currentHealth;

        if (m_currentHealth < (maxHealth / 2))
        {
            m_animator.SetBool("isStage2", true);
        }
    }

    public void ChangeColor()
    {
        m_spriteRenderer.color = new Color(217f, 68f, 68f);
    }

    public void BossTakeDamage(float damage)
    {
        if (isVunerable)
        {
            return;
        }
        takehit.Play();
        m_currentHealth -= damage;
        if (m_currentHealth <= 0)
        {
            m_animator.SetBool("isDead", true);
        }
    }
    public void OnDeadAnimation()
    {
        spike.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
