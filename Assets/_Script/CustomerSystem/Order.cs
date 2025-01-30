using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Ordering/Order")]
public class Order : ScriptableObject
{
    public int recipeNumber; //order number for phrases and to identify recipes
    public CraftingItem itemRequest; //which item is requested
    public int cost; //how much money player would get upon finishing an order
}
