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
    public AudioSource audioSourceFx;
    [SerializeField]
    protected MeowObject meowObject;
    public MeowObject MeowObject => meowObject;
    public LayerMask groundCheckLayer;

    protected DamageScriptable activeDamageBoostItem;
    private float damageBoostTimer;

    private List<ItemScriptable> eternalItems;


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
        eternalItems = new List<ItemScriptable>();
    }

    protected virtual void Update()
    {
        moveController.Update();

        if (activeDamageBoostItem)
        {
            damageBoostTimer += Time.deltaTime;
            if (damageBoostTimer >= 10)
            {
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

    public virtual void Switch(InputAction.CallbackContext context)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("isDodge"))
        {
            MeowObjectManager.Instance.Switch();
        }
    }

    protected void attack_timmer(string trigger)
    {
        if (!IsVulnerable) return;
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger(trigger);
            nextAttackTime = Time.time + 1f / meowObject.AttackRate;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        TakeDamage(10f); // TODO: ScriptableEnemy.Damage here
    //    }
    //}

    public override void Heal(float amount)
    {
        audioSourceFx.Play();
        health = Mathf.Min(MeowObject.Health, health + amount);
        print("Health: " + health);
    }

    public void BoostDamage(DamageScriptable boost)
    {
        audioSourceFx.Play();

        activeDamageBoostItem = boost;
        damageBoostTimer = 0;
        UpdateStats();
        print($"Base damage: {meowObject.Damage}; Boosted: {damage}");
    }

    public void AddEternalItem(ItemScriptable item)
    {
        audioSourceFx.Play();
        eternalItems.Add(item);
        UpdateStats();
    }

    public void UpdateStats()
    {
        // Reset damage & speed
        damage = meowObject.Damage;
        moveController.moveSpeed = meowObject.MoveSpeed;
        moveController.jumpForce = meowObject.JumpForce;

        // Add bonus damage
        if (activeDamageBoostItem)
        {
            damage += activeDamageBoostItem.amount;
        }

        // Add bonus speed
        if (moveController.activeSpeedBoostItem)
        {
            moveController.moveSpeed += moveController.activeSpeedBoostItem.amount;
        }

        // Add bonus jump force
        if (moveController.activeJumpForceItem)
        {
            moveController.jumpForce += moveController.activeJumpForceItem.amount;
        }

        // Add eternal damage, speed & jump force
        foreach (var eternalItem in eternalItems)
        {
            if (eternalItem is DamageScriptable)
            {
                damage += (eternalItem as DamageScriptable).amount;
            }
            else if (eternalItem is SpeedScriptable)
            {
                moveController.moveSpeed += (eternalItem as SpeedScriptable).amount;
            }
            else if (eternalItem is JumpForceScriptable)
            {
                moveController.jumpForce += (eternalItem as JumpForceScriptable).amount;
            }
        }
    }
}