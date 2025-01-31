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

    public float maxTimer = 30f;  // Начальное время таймера для клиента
    [Header("Сложность таймера")]
    public float timerDecrement = 2f; // На сколько укорачиваем каждый новый заказ
    public float minTimer = 10f;      // Минимальное время, до которого таймер может дойти
    public bool hasActiveCustomer = false;
    private float currCountdownValue;
    private Slider timerSlider;
    private CustomerManager customerManager;
    private Coroutine countdownCoroutine; // Хранит ссылку на запущенный таймер

public GameObject gameOverScreen;
    void Awake()
    {
        customerManager = FindFirstObjectByType<CustomerManager>();
        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>();
        gameOverScreen.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(CustomerSpawnDelay());
    }

    IEnumerator CustomerSpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        customerManager.SpawnCustomer();
    }

    public void GameOver() 
    {
        hasActiveCustomer = false;
        StopCountdown();
        gameOverScreen.SetActive(true);
    }

    // Вызываем при появлении нового клиента — запускает отсчёт его времени
    public void StartCountdown(float countdownValue)
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine); // Останавливаем старый таймер, если он есть
        }
        countdownCoroutine = StartCoroutine(CountdownRoutine(countdownValue));
    }

    private IEnumerator CountdownRoutine(float countdownValue)
    {
        currCountdownValue = countdownValue;
        timerSlider.maxValue = countdownValue;
        timerSlider.value = countdownValue;

        while (currCountdownValue > 0)
        {
            timerSlider.value = currCountdownValue;
            yield return new WaitForSecondsRealtime(1f); // Используем реальное время, чтобы паузы не сбивали таймер
            currCountdownValue--;
        }

        // Если таймер дотикал до 0, считаем, что игрок не успел — вызываем следующий
        customerManager.FailOrder();
    }

    public void ResetTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        timerSlider.value = maxTimer;
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }


    /// <summary>
    /// Уменьшаем время для следующего клиента, но не ниже minTimer.
    /// Вызываем из CustomerManager, когда заказ закончен (успех/провал).
    /// </summary>
    public void DecreaseTimerForNextCustomer()
    {
        maxTimer -= timerDecrement;
        if (maxTimer < minTimer)
        {
            maxTimer = minTimer;
        }
        Debug.Log("Новый таймер: " + maxTimer);
    }
}
