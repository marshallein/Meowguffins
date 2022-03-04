using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public ObjectPoolingScript pool;
    [SerializeField]
    private bool _canSpawn = true;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_canSpawn)
        {
            SpawnEnemy();
        } else
        {
            return;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = pool.GetGameObjectFromPool();

        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.gameObject.GetComponent<EnenyMeleeBehaviour>().leftLimit = this.leftLimit;
            enemy.gameObject.GetComponent<EnenyMeleeBehaviour>().rightLimit = this.rightLimit;

            enemy.SetActive(true);
            _canSpawn = false;
        }
    }
}
