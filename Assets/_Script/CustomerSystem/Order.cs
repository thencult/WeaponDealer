using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Ordering/Order")]
public class Order : ScriptableObject
{
    public int recipeNumber; //order number for phrases and to identify recipes
    public CraftingItem itemRequest; //which item is requested
    public int cost; //how much money player would get upon finishing an order


    //вместо itemRequest сделать короче список с тегами, что нужно заказу, типо, клиент просит острое и лёгкое, и в заказе будет ['sharp','light'], например, и допустим, каждый материал будет иметь как и нужный нам аттрибут, так и другие какие-то, которые клиент например не хочет, и тут можно например сделать как в таумкрафте, когда слишком много лишнего может привести к неприятным последствиям, как заражение, и тут это можно обыграть тем, что ты добавляешь в некий котёл определённое количество элементов, и другими гасишь, и тебе нужно найти баланс между материалами, чтобы сделать точный заказ


}
