using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Meow", menuName = "MeowObject")]
public class MeowObject : ScriptableObject
{
    public string prefabName;
    public GameObject prefab;
    public Sprite menuSprite;

    [SerializeField]
    private float moveSpeed = 7f;
    public float MoveSpeed => moveSpeed;
    [SerializeField]
    private float jumpForce = 23f;
    public float JumpForce => jumpForce;
    [SerializeField]
    private float wallJumpTime = 0.02f;
    public float WallJumpTime => wallJumpTime;
    [SerializeField]
    private float wallSlideSpeed = 0.06f;
    public float WallSlideSpeed => wallSlideSpeed;
    [SerializeField]
    private float wallDistance = 0.7f;
    public float WallDistance => wallDistance;
    [Header("Dodge")]
    [SerializeField]
    private float dodgeTime = 0.2f;
    public float DodgeTime => dodgeTime;
    [SerializeField]
    private float dodgeDistance = 15f;
    public float DodgeDistance => dodgeDistance;
    [SerializeField]
    private float dodgeCooldown = 2f;
    public float DodgeCooldown => dodgeCooldown;
    [SerializeField]
    private float attackRate = 2f;
    public float AttackRate => attackRate;
    [SerializeField]
    private float damage = 25f;
    public float Damage => damage;
    [SerializeField]
    private float health = 120f;
    public float Health => health;

}
