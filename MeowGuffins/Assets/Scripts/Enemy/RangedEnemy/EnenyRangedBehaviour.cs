using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyRangedBehaviour : EnenyMeleeBehaviour
{

    private ObjectPoolingScript poolingScript;
    private new void Awake()
    {
        poolingScript = GetComponentInChildren<ObjectPoolingScript>();
        base.Awake();
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

    public override void OnMove()
    {

        animator.SetBool("isWalk", true);

        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }

}
