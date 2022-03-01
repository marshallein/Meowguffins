using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MeowObjectManager : MonoBehaviour
{
    //public GameObject[] entityToSpawn;
    public MeowObject[] spawnManagerValues;
    public Vector3[] spawnPoints;
    public int NumberOfMeow = 2;
    public int moveSpeed;
    private void Start()
    {
        SetProperties();
        SpawnEntities();
        
    }
    void SetProperties()
    {
    }
    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;
        for (int i = 0; i < NumberOfMeow; i++)
        {        
            GameObject currentEntity = Instantiate(spawnManagerValues[i].prefab, spawnPoints[currentSpawnPointIndex], Quaternion.identity);
            currentEntity.name = spawnManagerValues[i].prefabName;
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % NumberOfMeow; 
            currentEntity.SetActive(true);
          
            
        }       
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {

        }
    }
}
