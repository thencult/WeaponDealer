using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
<<<<<<< Updated upstream
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
=======
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
=======
>>>>>>> Stashed changes
=======
    private bool isInsideDropZone = false; // Флаг, находится ли предмет в DropZone
    
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
    public CraftingItem itemData; // Assign the ScriptableObject for this item
    public bool isCombining = false; // Prevent duplicate combinations
<<<<<<< Updated upstream
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs

    void OnMouseDown()
    {
        StartDragging();
    }

    public void StartDragging()
    {
        isDragging = true;
=======

    void OnMouseDown()
    {
>>>>>>> Stashed changes
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, 0);
    }

<<<<<<< Updated upstream
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
    void OnMouseUp()
    {
        isDragging = false;

        if (!isInsideDropZone)
=======
=======
>>>>>>> Stashed changes
    void OnMouseDrag()
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0) + offset;
=======
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0) + offset;
    }

    void OnMouseUp()
    {
        if (!isInsideDropZone) // Если предмет не в зоне стола, удалить его
<<<<<<< Updated upstream
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
=======
>>>>>>> Stashed changes
        {
            Debug.Log($"{gameObject.name} удалён, так как он вне DropZone!");
            Destroy(gameObject);
        }
<<<<<<< Updated upstream
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
=======
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
    }

    void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("DraggableItem") && !isCombining && other.gameObject != gameObject)
=======
        if (other.CompareTag("DraggableItem") && !isCombining && other.gameObject != gameObject) // Ignore self
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
<<<<<<< Updated upstream
        else if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = true;
        }
    }
=======
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
=======
>>>>>>> Stashed changes
    }
=======
        else if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = true; // Предмет теперь в зоне стола
        }
    }
<<<<<<< Updated upstream
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
=======
>>>>>>> Stashed changes

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream:Assets/_Script/DraggableItem.cs
            isInsideDropZone = false;
        }
    }
}
=======
=======
>>>>>>> Stashed changes
            isInsideDropZone = false; // Предмет вышел из зоны стола
        }
    }
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
}
<<<<<<< Updated upstream
>>>>>>> Stashed changes:Assets/_Script/Other/DraggableItem.cs
=======
>>>>>>> Stashed changes
