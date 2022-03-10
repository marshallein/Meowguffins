using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> listItems;
    public List<Transform> spawnPositions;

    private static ShopManager _instance;
    public static ShopManager Instance { get => _instance; }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in spawnPositions)
        {
            int randomItem = Random.Range(0, listItems.Count);
            Instantiate(listItems[randomItem], t.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
