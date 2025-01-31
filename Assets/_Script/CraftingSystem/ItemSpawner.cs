using UnityEngine;

[RequireComponent(typeof(Collider2D))] // чтобы OnMouseDown/OnMouseDrag/OnMouseUp работали
public class ItemSpawner : MonoBehaviour
{
    public CraftingItem itemToSpawn;    // Данные о предмете (ScriptableObject)
    public GameObject itemPrefab;       // Префаб для спауна
    public Sprite idleSprite;           // Спрайт «мешка», когда мышь не наведена
    public Sprite hoverSprite;          // Спрайт «мешка» при наведении мыши
    private SpriteRenderer spriteRenderer;

    private DraggableItem currentSpawnedItem = null;
    private Vector3 offset;             // Смещение между курсором и позицией предмета
    private bool isDragging = false;    // Флаг: сейчас «тянем» новый предмет?

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && idleSprite)
            spriteRenderer.sprite = idleSprite;
    }

    private void OnMouseEnter()
    {
        if (spriteRenderer && hoverSprite)
            spriteRenderer.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        if (spriteRenderer && idleSprite)
            spriteRenderer.sprite = idleSprite;
    }

    private void OnMouseDown()
    {
        // При нажатии на "мешок" сразу спавним предмет
        if (itemPrefab == null || itemToSpawn == null)
        {
            Debug.LogError("ItemPrefab или ItemToSpawn не назначены в инспекторе!");
            return;
        }

        // Создаем предмет под курсором
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject newItemObj = Instantiate(itemPrefab, mousePos, Quaternion.identity);

        // Ищем DraggableItem на созданном объекте и присваиваем ScriptableObject
        if (newItemObj.TryGetComponent(out DraggableItem draggable) &&
            newItemObj.TryGetComponent(out SpriteRenderer newItemSprite))
        {
            draggable.itemData = itemToSpawn;
            newItemSprite.sprite = itemToSpawn.icon;
            newItemObj.name = itemToSpawn.name;

            // Запоминаем, что сейчас "тянем" этот предмет
            currentSpawnedItem = draggable;
            // Смещение между центром предмета и позицией мыши
            offset = newItemObj.transform.position - mousePos;
            isDragging = true;
        }
        else
        {
            Debug.LogError("На префабе предмета отсутствуют необходимые компоненты!");
        }
    }

    private void OnMouseDrag()
    {
        // Пока зажата мышь над мешком, двигаем предмет
        if (isDragging && currentSpawnedItem != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            currentSpawnedItem.transform.position = mousePos + offset;
        }
    }

    private void OnMouseUp()
    {
        // Отпускаем предмет
        if (isDragging && currentSpawnedItem != null)
        {
            // Если хотите, можете тут же проверить, в DropZone ли предмет
            // И при необходимости его удалить. Или пусть это делает DraggableItem.
            currentSpawnedItem = null;
        }

        isDragging = false;
    }
}
