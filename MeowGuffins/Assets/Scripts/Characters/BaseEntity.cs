using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    protected Animator animator;
    float nextAnimTime = 0f;
    float frameRate = 2f;

    protected float health;
    public float Health { get => health; }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayDeathAnimation();
        } else
        {
            PlayHitAnimation();
        }
        
    }

    public virtual void PlayHitAnimation() {
        if (Time.time < nextAnimTime) return;

        animator.SetTrigger("take_damage");
        nextAnimTime = Time.time + 1f / frameRate;
    }

    // This function also triggers BaseEnemy.OnDeath on finished
    public virtual void PlayDeathAnimation() {
        animator.SetTrigger("isDeath2");
    }
}
