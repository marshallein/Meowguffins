using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyRangedBehaviour : EnenyMeleeBehaviour
{

    private ObjectPoolingScript poolingScript;
    private void Awake()
    {
        poolingScript = GetComponentInChildren<ObjectPoolingScript>();
        base.Awake();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public void AttackAttached()
    {
        StartCoroutine(StartThunderAttack());
    }

    public IEnumerator StartThunderAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < 5; i++)
        {
            if (player.activeInHierarchy)
            {
                GameObject thunder = poolingScript.GetGameObjectFromPool();
                if (thunder != null)
                {
                    thunder.transform.position = player.transform.position;
                    thunder.SetActive(true);
                }
            }
            yield return new WaitForSeconds(0.5f);

        }
    }
}
