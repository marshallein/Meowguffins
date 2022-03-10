using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShopItemManager : MonoBehaviour
{
    public ItemScriptable item;
    private Transform spawnPoint;

    public void SelectItem()
    {
        Debug.Log("Purchase clicked");
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
        Instantiate(item.itemPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Successfully purchase");
    }

}
