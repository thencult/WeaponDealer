using UnityEngine;

public class ObjectPile : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab for the object to "take"
    public Transform spawnPoint;   // Optional: where the object spawns (useful for animation)

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        SpawnDraggableObject();
    }

    private void SpawnDraggableObject()
    {
        // Create a new object instance
        GameObject newObject = Instantiate(objectPrefab, spawnPoint ? spawnPoint.position : transform.position, Quaternion.identity);

        // Enable dragging on the new object
        Draggable draggable = newObject.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.StartDragging(); // Begin dragging immediately
        }
    }
}
