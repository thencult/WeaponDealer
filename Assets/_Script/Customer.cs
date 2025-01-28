using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Ordering/Customer")]
public class Customer : ScriptableObject
{
    public Sprite customerSprite; //sprite for Customer object
    public Order order; //an assigned order
    public bool isDangerous = false; //is this customer a knight or some sort of a cop?
}
