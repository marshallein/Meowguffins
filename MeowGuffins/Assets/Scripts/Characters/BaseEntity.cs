using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    float nextAnimTime = 0f;
    float frameRate = 2f;
    public AudioSource takeHitFx;

    protected float health;
    public float Health { get => health; }
    public bool IsVulnerable { get; set; }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        IsVulnerable = true;
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsVulnerable)
        {
            health -= damage;
            takeHitFx.Play();
            if (health <= 0)
            {
                PlayDeathAnimation();
            }
            else
            {
                PlayHitAnimation();
            }
        }
    }

    public virtual void PlayHitAnimation()
    {
        if (Time.time < nextAnimTime) return;

        animator.SetTrigger("take_damage");
        nextAnimTime = Time.time + 1f / frameRate;
    }

    // This function also triggers BaseEnemy.OnDeath on finished
    public virtual void PlayDeathAnimation()
    {
        animator.SetTrigger("isDeath2");
    }

    public virtual void Heal(float amount)
    {
        health += amount;
    }
}
