using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseMeow : BaseEntity
{
    public Transform groundCheck;
    public Transform frontCheck;

    public MeowMoveController moveController;
    [SerializeField]
    protected MeowObject meowObject;
    public MeowObject MeowObject => meowObject;
    public LayerMask groundCheckLayer;

    protected float nextAttackTime = 0f;

    public float MaxHealth { get => MeowObject.Health; }

    public float AttackDamage { get => MeowObject.Damage; }

    public bool IsFacingRight { get => moveController.m_isFacingRight; }

    protected override void Awake()
    {
        base.Awake();
        moveController = new MeowMoveController(this);
        health = MeowObject.Health;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        moveController.Update();
    }

    protected virtual void FixedUpdate()
    {
        moveController.FixedUpdate();
    }

    public void OnDeath()
    {
        Destroy(this);
    }

    public void OnEndOfDodge()
    {
        IsVulnerable = true;
    }

    public void OnReadCharacterMove(InputAction.CallbackContext context)
    {
        moveController.OnReadCharacterMove(context);
    }

    public void OnCharacterJump(InputAction.CallbackContext context)
    {
        moveController.OnCharacterJump(context);
    }

    public virtual void OnCharacterDodge(InputAction.CallbackContext context)
    {
        IsVulnerable = false;
        moveController.OnCharacterDodge(context);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, 0.15f);
    }

    public virtual void OnCharacterAttack(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set");
    }

    public virtual void OnCharacterAttack2(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set2");
    }

    public virtual void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set3");
    }

    public virtual void OnCharacterAttack4(InputAction.CallbackContext context)
    {
        attack_timmer("attack_set4");
    }

    protected void attack_timmer(string trigger)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger(trigger);
            nextAttackTime = Time.time + 1f / meowObject.AttackRate;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Damageable")
    //    {
    //        if (collision.GetType() == typeof(BoxCollider2D))
    //        {
    //            Debug.Log("hit" + collision.ToString());
    //            TakeDamage(10f); // TODO: ScriptableEnemy.Damage here
    //        }
    //    }
    //}
}
