using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseMeow : BaseEntity
{
    public Transform groundCheck;
    public Transform frontCheck;
    public List<string> inventory;
    public MeowMoveController moveController;
    [SerializeField]
    protected MeowObject meowObject;
    public MeowObject MeowObject => meowObject;
    public LayerMask groundCheckLayer;

    protected DamageScriptable activeDamageBoostItem;
    private float damageBoostTimer;
    private float damage;
    public float Damage { get => damage; }

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
        inventory = new List<string>();
    }

    protected virtual void Update()
    {
        moveController.Update();

        if (activeDamageBoostItem)
        {
            damageBoostTimer += Time.deltaTime;
            if (damageBoostTimer >= 10)
            {
                damage = meowObject.Damage;
                damageBoostTimer = 0;
                activeDamageBoostItem = null;
                print("Damage Boost expired.");
            }
        }
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
        print("attack 1 ran");
        attack_timmer("attack_set");
    }

    public virtual void OnCharacterAttack2(InputAction.CallbackContext context)
    {
        print("attack 2 ran");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10f); // TODO: ScriptableEnemy.Damage here
        }
    }

    public override void Heal(float amount)
    {
        health = Mathf.Min(MeowObject.Health, health + amount);
        print("Health: " + health);
    }

    public void BoostDamage(DamageScriptable boost)
    {
        activeDamageBoostItem = boost;
        damageBoostTimer = 0;
        damage = meowObject.Damage + boost.amount;
        print($"Base damage: {meowObject.Damage}; Boosted: {damage}");
    }
}
