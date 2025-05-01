using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public CraftingItem item1; // First ingredient
    public CraftingItem item2; // Second ingredient
    public Tool usedTool;
    public CraftingItem result; // Resulting item
}
