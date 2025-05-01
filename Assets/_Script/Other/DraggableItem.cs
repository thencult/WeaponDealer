using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
    private bool isInsideDropZone = false; // Флаг, находится ли предмет в DropZone

    public Item itemData; // Assign the ScriptableObject for this item
    public bool isCombining = false; // Prevent duplicate combinations
    private GameManager gameManager; // Ссылка на GameManager

    //scaling effect
    Vector2 originalLocalScale;
    float scaleCoefficient = 1.2f;
    float scaledX;
    float scaledY;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Start()
    {
        originalLocalScale = transform.localScale;
        scaledX = originalLocalScale.x * scaleCoefficient;
        scaledY = originalLocalScale.y * scaleCoefficient;
    }
    void OnMouseEnter()
    {
        transform.DOScale(scaledX, 0.1f);
        transform.DOScale(scaledY, 0.1f);
        // Mathf.Lerp(originalLocalScale.x, scaledX, 0.1f);
        // Mathf.Lerp(originalLocalScale.y, scaledY, 0.1f);
    }
    void OnMouseExit()
    {
        // Mathf.Lerp(scaledX, originalLocalScale.x, 0.1f);
        // Mathf.Lerp(scaledY, originalLocalScale.y, 0.1f);
        transform.DOScale(originalLocalScale.x, 0.1f);
        transform.DOScale(originalLocalScale.y, 0.1f);
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
        // transform.parent = null;
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
        if (other.CompareTag("MaterialCard") && !isCombining && other.gameObject != gameObject) // Ignore self
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

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {
            isInsideDropZone = false; // Предмет вышел из зоны стола
        }
    }
}
