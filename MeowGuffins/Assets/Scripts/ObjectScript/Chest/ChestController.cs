using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public const int NumberOfItemsToRoll = 3;

    public bool isOpen = false;
    public bool isSpawmed = true;
    public Animator animator;
    public Transform itemSpawnPoint;
    public List<ItemScriptable> items;

    private void FixedUpdate()
    {
        if (isOpen)
        {
            Debug.Log("Chest is open");
            animator.SetBool("isOpen", isOpen);
            if (isSpawmed)
            {
                SpawnItems();
            }
            isOpen = false;
            isSpawmed = false;
        }
    }

    public void OpenChest()
    {
        isOpen = true;
    }
    public void SpawnItems()
    {
        var itemsToRoll = new List<ItemScriptable>();
        items.ForEach(itemsToRoll.Add);

        int numberOfItemsToRoll = System.Math.Min(itemsToRoll.Count, NumberOfItemsToRoll);

        while (numberOfItemsToRoll-- > 0)
        {
            int rollIndex = Random.Range(0, itemsToRoll.Count);
            var rolledItem = itemsToRoll[rollIndex];

            // Exclude the rolled item from random list
            itemsToRoll.RemoveAt(rollIndex);

            var spawnPoint = itemSpawnPoint.position;
            var item = Instantiate(rolledItem.itemPrefab, spawnPoint, Quaternion.identity);
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

            var randomDirectionPoint = spawnPoint +
                new Vector3(Random.Range(spawnPoint.x - 1f, spawnPoint.x + 1f), 2f);
            var directionVector = (randomDirectionPoint - spawnPoint).normalized;

            rb.AddForce(directionVector * 100, ForceMode2D.Force);
        }
    }
}
