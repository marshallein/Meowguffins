using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingScript : MonoBehaviour
{

    public int amountToPool;
    private List<GameObject> _pools = new List<GameObject>();

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
            _pools.Add(pool);
        }
    }

    public GameObject GetGameObjectFromPool()
    {
        foreach (GameObject pool in _pools)
        {
            if (!pool.activeInHierarchy)
            {
                return pool;
            }
        }
        return null;
    }
}
