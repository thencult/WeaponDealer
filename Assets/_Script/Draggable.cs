using UnityEngine;
using UnityEngine.EventSystems; // For handling UI interactions

public class Draggable : MonoBehaviour
{
    public enum ObjectType { SwordBase, EffectElement, Money }
    public ObjectType objectType;

    private bool isDragging = false;
    private Vector3 startPosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    public void StartDragging()
    {
        startPosition = transform.position; // Save the start position
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);
            transform.position = mainCamera.ScreenToWorldPoint(mousePosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (!IsValidDropZone())
        {
            transform.position = startPosition; // Return to original position
        }
        else
        {
            HandleDrop();
        }
    }

    private bool IsValidDropZone()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        HandleDrop();
        return hit != null && hit.CompareTag("DropZone");
    }

    private void HandleDrop()
    {
        Debug.Log($"Dropped {objectType}!");
        Destroy(this.gameObject);
    }
}
