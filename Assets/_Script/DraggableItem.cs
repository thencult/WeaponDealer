using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
    private bool isInsideDropZone = false;
    private bool isDragging = false; // Новый флаг для авто-перетаскивания

    public CraftingItem itemData;
    public bool isCombining = false;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0) + offset;
        }
    }

    void OnMouseDown()
    {
        StartDragging();
    }

    public void StartDragging()
    {
        isDragging = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (!isInsideDropZone)
        {
            Debug.Log($"{gameObject.name} удалён, так как он вне DropZone!");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DraggableItem") && !isCombining && other.gameObject != gameObject)
        {
            DraggableItem otherItem = other.GetComponent<DraggableItem>();
            if (otherItem != null && !otherItem.isCombining)
            {
                isCombining = true;
                otherItem.isCombining = true;

                Debug.Log($"{gameObject.name} collided with {other.gameObject.name}");

                RecipeManager recipeManager = FindFirstObjectByType<RecipeManager>();
                if (recipeManager != null)
                {
                    Debug.Log("Attempting to combine items...");
                    recipeManager.TryCombine(gameObject, other.gameObject);
                }
                else
                {
                    Debug.LogWarning("RecipeManager not found in the scene!");
                }
            }
        }
        else if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = false;
        }
    }
}