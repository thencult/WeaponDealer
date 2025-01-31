using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class CustomerManager : MonoBehaviour
{
    public string[] phrases; 
    public Customer[] customerData; 
    public GameObject textBubble; 
    public TextMeshProUGUI textBubbleText; 
    public Order desiredOrder; 
    CraftingItem desiredOrderitem; 
    public CraftingItem givenOrder; 
    OrderManager orderManager; 
    SpriteRenderer spriteRenderer; 
    GameManager gameManager;
    Animator animator;
    
    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 

        if (orderManager == null)
            orderManager = FindAnyObjectByType<OrderManager>();
        
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
    }

    //================ RANDOMIZERS ================
    int randomOrderIndex() { return Random.Range(0, orderManager.possibleOrders.Length); }
    int randomCustomerIndex() { return Random.Range(0, customerData.Length); }
    string PickRandomPhrase() {
        int randomPhraseIndex = Random.Range(0, phrases.Length);
        return phrases[randomPhraseIndex];
    }
    //=============================================

    public void MakeOrder()
    {
        desiredOrder = orderManager.possibleOrders[randomOrderIndex()];
        desiredOrderitem = desiredOrder.itemRequest;
        textBubble.SetActive(true);
        textBubbleText.text = PickRandomPhrase() + " " + desiredOrderitem.name;
    }

    public void SpawnCustomer()
    {
        int fixedCustomer = randomCustomerIndex();
        spriteRenderer.sprite = customerData[fixedCustomer].customerSprite;
        gameObject.name = customerData[fixedCustomer].name;
        animator.SetTrigger("Spawn");
        gameManager.hasActiveCustomer = true;
        gameManager.StartCountdown(gameManager.maxTimer);
        MakeOrder();
        // Запускаем таймер для этого клиента
        gameManager.StartCountdown(gameManager.maxTimer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var draggableItem = other.gameObject.GetComponent<DraggableItem>();
        if (draggableItem == null) return;

        if (draggableItem.itemData == desiredOrderitem) {
            CompleteOrder();
            Destroy(other.gameObject);
        }
        else {
            FailOrder();
            Destroy(other.gameObject);
        }
    }

public void CompleteOrder()
{
    Debug.Log("Order Complete");
    // Останавливаем таймер немедленно
    gameManager.StopCountdown();

    StartCoroutine(DelayedTextClear());
    gameManager.money += desiredOrder.cost;
    gameManager.coinCounter.text = "Score: " + gameManager.money.ToString();

    NextCustomer(); 
}

public void FailOrder()
{
    Debug.Log("Order Failed");
    // Останавливаем таймер немедленно
    gameManager.StopCountdown();

    gameManager.health--;
    if (gameManager.health == 3) {
        gameManager.heart_one.SetActive(true);
        gameManager.heart_two.SetActive(true);
        gameManager.heart_three.SetActive(true);
    }
    else if (gameManager.health == 2) {
        gameManager.heart_one.SetActive(false);
        gameManager.heart_two.SetActive(true);
        gameManager.heart_three.SetActive(true);
    }
    else if (gameManager.health == 1) {
                gameManager.heart_one.SetActive(false);
        gameManager.heart_two.SetActive(false);
        gameManager.heart_three.SetActive(true);
    }


    textBubbleText.text = "This is not what I wanted!";
    if (gameManager.health <= 0) {
        gameManager.GameOver();
    }
    StartCoroutine(DelayedNextCustomer());
}


    public void NextCustomer()
{
    // Перед тем, как «уходит» старый клиент, можно сразу выключить флаг
    gameManager.hasActiveCustomer = false;

    animator.SetTrigger("Go Away");
    gameManager.DecreaseTimerForNextCustomer();
    StartCoroutine(DelayedSpawnCustomer());
}
    IEnumerator DelayedNextCustomer()
    {
        yield return new WaitForSeconds(2.0f);
        NextCustomer();
    }

    IEnumerator DelayedSpawnCustomer()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnCustomer();
    }

    IEnumerator DelayedTextClear()
    {
        textBubbleText.text = "Thank you!";
        yield return new WaitForSeconds(1.0f);
        textBubbleText.text = "";
    }
}
