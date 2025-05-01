using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public int money; // Total money count
    [Range(0,3)]
    public int health = 3;
    public GameObject heart_one;
    public GameObject heart_two;
    public GameObject heart_three;
    public TextMeshProUGUI coinCounter;
    public int completedOrderAmount; // Completed order amount, for stats
    public bool hasActiveCustomer = false;
    private CustomerManager customerManager;

public GameObject gameOverScreen;
    void Awake()
    {
        customerManager = FindFirstObjectByType<CustomerManager>();

        gameOverScreen.SetActive(false);
    }

    void Start()
    {

    }

    IEnumerator CustomerSpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        customerManager.SpawnCustomer();
    }

    public void GameOver() 
    {
        hasActiveCustomer = false;
        gameOverScreen.SetActive(true);
    }

    // Вызываем при появлении нового клиента — запускает отсчёт его времени

}
