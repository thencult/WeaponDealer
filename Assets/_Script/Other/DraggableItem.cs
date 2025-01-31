using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
    private bool isInsideDropZone = false; // Флаг, находится ли предмет в DropZone
    
    public CraftingItem itemData; // Assign the ScriptableObject for this item
    public bool isCombining = false; // Prevent duplicate combinations
    private GameManager gameManager; // Ссылка на GameManager

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, 0);

                if (!gameManager.hasActiveCustomer)
        {
            // Просто игнорируем клик, либо можно вывести сообщение
            return;
        }
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0) + offset;

        if (!gameManager.hasActiveCustomer)
        {
            // Просто игнорируем клик, либо можно вывести сообщение
            return;
        }
    }

    void OnMouseUp()
    
    {

        if (!gameManager.hasActiveCustomer)
        {
            return;
        }
        if (!isInsideDropZone) // Если предмет не в зоне стола, удалить его
        {
            Debug.Log($"{gameObject.name} удалён, так как он вне DropZone!");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DraggableItem") && !isCombining && other.gameObject != gameObject) // Ignore self
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
            isInsideDropZone = true; // Предмет теперь в зоне стола
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = false; // Предмет вышел из зоны стола
        }
    }
}
