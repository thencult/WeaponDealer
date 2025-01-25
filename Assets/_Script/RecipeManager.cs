using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<CraftingRecipe> recipes; // Assign all recipes in the Inspector

    public GameObject itemPrefab; // Prefab for spawned items

public void TryCombine(GameObject item1, GameObject item2)
{
    DraggableItem data1 = item1.GetComponent<DraggableItem>();
    DraggableItem data2 = item2.GetComponent<DraggableItem>();

    if (data1 == null || data2 == null)
    {
        Debug.LogWarning("One or both items are missing the DraggableItem component.");
        ResetCombiningState(data1, data2);
        return;
    }

    Debug.Log($"Checking combination: {data1.itemData.itemName} + {data2.itemData.itemName}");

    foreach (CraftingRecipe recipe in recipes)
    {
        Debug.Log($"Checking recipe: {recipe.item1.itemName} + {recipe.item2.itemName} = {recipe.result.itemName}");

        if ((recipe.item1 == data1.itemData && recipe.item2 == data2.itemData) ||
            (recipe.item1 == data2.itemData && recipe.item2 == data1.itemData))
        {
            Debug.Log($"Recipe matched! Combining {data1.itemData.itemName} and {data2.itemData.itemName} to create {recipe.result.itemName}");

            Vector3 spawnPosition = (item1.transform.position + item2.transform.position) / 2;

            Destroy(item1);
            Destroy(item2);

            SpawnItem(recipe.result, spawnPosition);

            // Reset the combining state after successful combination
            return;
        }
    }

    Debug.Log("No matching recipe found.");
    ResetCombiningState(data1, data2); // Reset flags if no match is found
}

private void ResetCombiningState(DraggableItem data1, DraggableItem data2)
{
    if (data1 != null) data1.isCombining = false;
    if (data2 != null) data2.isCombining = false;
}



public void SpawnItem(CraftingItem item, Vector3 position)
{
    Debug.Log($"Spawning new item: {item.itemName} at position {position}");

    // Instantiate the prefab
    GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);

    // Assign the item's data and sprite
    if (newItem.TryGetComponent(out DraggableItem draggable) &&
        newItem.TryGetComponent(out Collider2D collider2d) &&
        newItem.TryGetComponent(out SpriteRenderer spriteRenderer))
    {
        draggable.itemData = item;
        spriteRenderer.sprite = item.icon;
        collider2d.isTrigger = true;

        // Adjust the collider size to fit the sprite
        ResizeColliderToSprite(collider2d, spriteRenderer);

        Debug.Log($"{item.itemName} spawned successfully!");
    }
    else
    {
        Debug.LogError("Spawned item prefab is missing required components!");
    }
}

// Helper method to resize the collider
private void ResizeColliderToSprite(Collider2D collider, SpriteRenderer spriteRenderer)
{
    if (collider is BoxCollider2D boxCollider)
    {
        // Match the collider size to the sprite's size
        boxCollider.size = spriteRenderer.sprite.bounds.size;
        boxCollider.offset = spriteRenderer.sprite.bounds.center;
    }
    else if (collider is CircleCollider2D circleCollider)
    {
        // Use the larger dimension of the sprite bounds as the circle radius
        float radius = Mathf.Max(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y) / 2f;
        circleCollider.radius = radius;
    }
    else if (collider is PolygonCollider2D polygonCollider)
    {
        // Automatically regenerate the polygon shape based on the sprite
        polygonCollider.pathCount = 0; // Clear existing paths
        polygonCollider.SetPath(0, spriteRenderer.sprite.vertices);
    }
    else
    {
        Debug.LogWarning("Unsupported collider type for resizing!");
    }
}


}
