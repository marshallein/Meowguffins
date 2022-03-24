using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public float maxHeath;
    public AudioSource hitFx; 


    [SerializeField]
    private float currentHeath;
    private EnenyMeleeBehaviour m_enemyMeleeBehaviour;
    private Animator m_animator;
    private CoinScriptable coin;


    private void Awake()
    {
        currentHeath = maxHeath;
        m_enemyMeleeBehaviour = GetComponent<EnenyMeleeBehaviour>();
        m_animator = GetComponent<Animator>();
        coin = Resources.LoadAll<CoinScriptable>("Items")[0];
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float inputDamage)
    {
        m_enemyMeleeBehaviour.dazedTime = m_enemyMeleeBehaviour.startDazedTime;
        currentHeath -= inputDamage;
        if (this.gameObject.activeInHierarchy)
        {
            hitFx.Play();
        }
        if (currentHeath <= 0)
        {
            m_animator.SetTrigger("isDead");
            currentHeath = maxHeath;
            m_enemyMeleeBehaviour.AttackMode = false;

            var spawnPoint = transform.position;
            for (int i = 0; i < 3; i++)
            {
                var coinGO = Instantiate(coin.itemPrefab, spawnPoint, Quaternion.identity);
                Rigidbody2D rb = coinGO.GetComponent<Rigidbody2D>();

                var randomDirectionPoint = spawnPoint +
                    new Vector3(Random.Range(spawnPoint.x - 1f, spawnPoint.x + 1f), 2f);
                var directionVector = (randomDirectionPoint - spawnPoint).normalized;

                rb.AddForce(directionVector * 100, ForceMode2D.Force);
            }
        }
    }

}
