using UnityEngine;
using TMPro;
public class CustomerManager : MonoBehaviour
{
    public string[] phrases; //list of phrases customer say when are spawned (should be like "I'd like to order..." + order number)
    public Customer[] customerData; //list of customer data, a ScriptableObject which holds customer order, name, sprite etc. this object is just a place to show all the info, and where to press
    public GameObject textBubble; //text bubble object, used just to hide text bubble, but can be used to store backgrounds etc.
    public TextMeshProUGUI textBubbleText; //text of the bubble, where phrases are shown
    public Order desiredOrder; //a randomised order per customer
    CraftingItem desiredOrderitem; //item of that order
    public CraftingItem givenOrder; //variable for item, which player gives to the customer, information
    OrderManager orderManager; //an object with the order array
    SpriteRenderer spriteRenderer; //this object's sprite manager, used for storing all the orders in an array.
    GameManager gameManager;
    

    //==========RANDOM VALUES=========
    int randomOrderIndex() //randomizes orders
    {
        return Random.Range(0, orderManager.possibleOrders.Length);
    }
    int randomCustomerIndex() //randomizes customers
    {
        return Random.Range(0, customerData.Length);
    }
    string PickRandomPhrase() //randomizes the phrase to be told by the customer
    {
        int randomPhraseIndex = Random.Range(0, phrases.Length);
        return phrases[randomPhraseIndex];
    }
    //=================================


    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //assigns SpriteRenderer if it's disabled/not found

        if (orderManager == null)
            orderManager = FindAnyObjectByType<OrderManager>(); //same for OrderManager
        
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
    }

    public void MakeOrder()
    {
        desiredOrder = orderManager.possibleOrders[randomOrderIndex()]; //choose an order
        desiredOrderitem = desiredOrder.itemRequest; //take an item out of it
        textBubble.SetActive(true); //show text bubble
        textBubbleText.text = PickRandomPhrase() + desiredOrderitem.name; //print randomized text on it
    }
    public void SpawnCustomer()
    {
        int fixedCustomer = randomCustomerIndex();
        spriteRenderer.sprite = customerData[fixedCustomer].customerSprite; //assigns Sprite, stored in customerData, to this object
        gameObject.name = customerData[fixedCustomer].name; //same for the name
        MakeOrder();
        gameManager.StartCountdown(gameManager.maxTimer);

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.GetComponent<DraggableItem>().itemData == desiredOrderitem) {
            CompleteOrder();
            Destroy(other.gameObject);
        }

    }

    public void CompleteOrder()
    {
        //basically needs to be: if given item mathes the desired order item, move on to the next customer and give player order cost money
        //if item isn't matching - next customer
        Debug.Log("Order Complete");
        gameManager.money += desiredOrder.cost;
        NextCustomer();
    }

    public void FailOrder() {
        Debug.Log("Order Failed");
        NextCustomer();
    }
    public void NextCustomer()
    {
        Debug.Log("Next Customer");
        SpawnCustomer();
    }

}
