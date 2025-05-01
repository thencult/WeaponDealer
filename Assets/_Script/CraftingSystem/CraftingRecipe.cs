using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public Item item1; // First ingredient
    public Item item2; // Second ingredient
    public Tool usedTool;
    public Item result; // Resulting item
}
