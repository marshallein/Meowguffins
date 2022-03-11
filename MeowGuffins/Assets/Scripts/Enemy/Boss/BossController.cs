using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform hitPosition;
    public LayerMask playerMask;
    public float rangeOfAttack;
    public bool isFlipped = false;
    public Transform LeftLimit;
    public Transform RightLimit;
    public ObjectPoolingScript thunderPool;
    public ObjectPoolingScript ghostPool;
    public float distanceBetweenThunder = 5f;
    public Transform ghostSpawn;


    public void LookAtPlayer(Transform player)
    {
        Vector3 flipped = transform.localScale;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void CreateThunder(Transform _boss)
    {
        float lastXpos = _boss.position.x;

        if (Mathf.Abs(_boss.position.x - lastXpos) < distanceBetweenThunder)
        {
            GameObject thunder = thunderPool.GetGameObjectFromPool();
            if (thunder != null)
            {
                thunder.transform.position = new Vector2(lastXpos, _boss.position.y);
                thunder.SetActive(true);
                lastXpos = _boss.position.x;
            }
        }
    }

    public void SpawnGhost()
    {
        StartCoroutine(SpawnGhostTimer());
    }

    private IEnumerator SpawnGhostTimer()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject ghost = ghostPool.GetGameObjectFromPool();
            if (ghost != null)
            {
                ghost.transform.position = ghostSpawn.position;
                ghost.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void AttackPlayer()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(hitPosition.position, rangeOfAttack, playerMask);

        if (colInfo != null)
        {
            Debug.Log(colInfo.name + "Hit");
            if (colInfo.gameObject.tag == "Player")
            {
                var player = colInfo.GetComponent<BaseMeow>();
                player.TakeDamage(30f);
            }
        }
    }

    public Transform OnSelectAttack(Transform _boss)
    {
        float distanceToLeft = Vector2.Distance(_boss.transform.position, LeftLimit.position);
        float distanceToRight = Vector2.Distance(_boss.transform.position, RightLimit.position);
        if (distanceToLeft > distanceToRight)
        {
            return LeftLimit;
        }
        else
        {
            return RightLimit;
        }
    }

    public bool InsideOfBound(Transform _boss)
    {
        return _boss.position.x > LeftLimit.position.x && _boss.position.x < RightLimit.position.x;
    }

    public void OnScreamAnimation()
    {
        CinemachineShake.Instance.Shake(1.5f, 10f);
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(hitPosition.position, rangeOfAttack);
    }

}
