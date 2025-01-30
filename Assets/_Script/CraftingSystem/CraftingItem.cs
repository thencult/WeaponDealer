using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Crafting/Item")]
public class CraftingItem : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite icon; // Item's sprite
}
