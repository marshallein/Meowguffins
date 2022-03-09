using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePoolingScript : MonoBehaviour
{

    public int amountToPool;
    private Queue<GameObject> _pools = new Queue<GameObject>();

    [SerializeField]
    private GameObject PrefabToPool;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject pool = Instantiate(PrefabToPool);
            pool.transform.parent = gameObject.transform;
            pool.SetActive(false);
            _pools.Enqueue(pool);
        }
    }

   public GameObject GetGameObjectFromQueuePool()
    {
        return _pools.Dequeue();
    }
}
