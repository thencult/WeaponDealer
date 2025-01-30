using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int money; // Total money count
    public int completedOrderAmount; // Completed order amount, for stats
    public float maxTimer = 30;
    private float currCountdownValue;

    private Slider timerSlider;
    private CustomerManager customerManager;
    private Coroutine countdownCoroutine; // Хранит ссылку на запущенный таймер

    void Awake()
    {
        customerManager = FindFirstObjectByType<CustomerManager>();
        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>();
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
            yield return new WaitForSecondsRealtime(1f); // Используем реальное время
            currCountdownValue--;
        }

        customerManager.NextCustomer();
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
}
