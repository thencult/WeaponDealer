using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public CraftingItem itemToSpawn; // The type of item to spawn (ScriptableObject)
    public GameObject itemPrefab;   // Prefab for the spawned item
    public Transform spawnPoint;   // Optional: Point where items will spawn
    public int maxSpawnedItems = 10; // Limit the number of spawned items
    private int currentSpawnedItems = 0;

    private void OnMouseDown()
    {
        if (currentSpawnedItems >= maxSpawnedItems)
        {
            Debug.LogWarning("Maximum spawned items reached for this spawner.");
            return;
        }

        SpawnItem();
    }

    public void SpawnItem()
    {
        if (itemPrefab == null || itemToSpawn == null)
        {
            Debug.LogError("ItemPrefab or ItemToSpawn is not assigned in the ItemSpawner!");
            return;
        }

        // Determine the spawn position
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;

        // Spawn the item
        GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);


        // Assign the item data and sprite to the spawned item
        if (newItem.TryGetComponent(out DraggableItem draggable) &&
            newItem.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            draggable.StartDragging();
            draggable.itemData = itemToSpawn;
            spriteRenderer.sprite = itemToSpawn.icon;
            newItem.gameObject.name = itemToSpawn.itemName;

            // Сразу начать перетаскивание предмета
            draggable.StartDragging();

            Debug.Log($"Spawned item: {itemToSpawn.itemName} at {position}");
        }
        else
        {
            Debug.LogError("The spawned item prefab is missing required components!");
        }

        currentSpawnedItems++;
    }
}
