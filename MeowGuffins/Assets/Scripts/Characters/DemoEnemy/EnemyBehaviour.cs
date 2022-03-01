using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BaseEntity
{
    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;
    public Transform target;
    public Transform checkPoint;
    public Transform firePoint;
    public GameObject bullet;
    private SpriteRenderer sp;
    private float moveSpeed = 2f;
    public float attackRange;
    public bool check = false;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        wayPointTarget = wayPoint1;
        InvokeRepeating("Shoot", 1, 1);
        health = 100f; // TODO: move this into ScriptableEnemy
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.Find("transform1").transform;
        }
        if (Vector2.Distance(checkPoint.position, target.position) <= attackRange)
        {
            check = true;
        }
        else
        {
            Patrol();
            check = false;
        }

    }

    private void Shoot()
    {
        if (check)
        {
            GameObject bulletSpawn = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bulletSpawn.GetComponent<Rigidbody2D>();
            rb.velocity = bulletSpawn.transform.right * -8;
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint1.position) <= 0.01)
        {
            wayPointTarget = wayPoint2;
            sp.flipX = true;
        }
        if (Vector2.Distance(transform.position, wayPoint2.position) <= 0.01)
        {
            wayPointTarget = wayPoint1;
            sp.flipX = false;
        }
    }
}
