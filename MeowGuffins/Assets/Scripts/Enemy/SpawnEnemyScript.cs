using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public ObjectPoolingScript pool;
    public bool spawnOneTime = true;

    private void FixedUpdate()
    {
        if (spawnOneTime)
        {
            SpawnEnemy();
        }
    }

    private void OnBecameInvisible()
    {
        StartCoroutine(DelayTimeToSpawn(5));
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
        }
        spawnOneTime = false;
    }


    public IEnumerator DelayTimeToSpawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        SpawnEnemy();
    }
}
