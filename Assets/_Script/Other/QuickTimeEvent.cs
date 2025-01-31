using UnityEngine;
using System;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    [Header("Основные настройки")]
    public float eventDuration = 3f;  // Сколько всего времени даётся на QTE
    public float moveSpeed = 2f;      // Скорость, с которой ползунок бегает по полосе

    [Header("UI-элементы")]
    public Slider qteSlider;          // Слайдер, по которому будет «бегать» ползунок (заполняться от 0 до 1)
    public RectTransform greenZone;   // Прямоугольник зелёной зоны (на том же слайдере или рядом)
    public GameObject qtePanel;       // Панель QTE, включающая все визуальные элементы (Slider, тексты и т.д.)

    // Позиции зелёной зоны [0..1] (левая граница, правая граница) — будем генерировать случайно
    private float greenZoneMin;
    private float greenZoneMax;

    // Текущее значение ползунка [0..1]
    private float sliderValue;
    private float direction = 1f; // 1 — вправо, -1 — влево

    // Система таймера
    private float timer;
    private bool qteActive;

    // Коллбэки на успех/провал
    public Action OnSuccess;
    public Action OnFail;

    void Update()
    {
        if (!qteActive) return;

        // Двигаем ползунок туда-сюда
        sliderValue += direction * moveSpeed * Time.deltaTime;
        if (sliderValue >= 1f)
        {
            sliderValue = 1f;
            direction = -1f; // меняем направление
        }
        else if (sliderValue <= 0f)
        {
            sliderValue = 0f;
            direction = 1f;  // меняем направление
        }

        // Отобразить новое значение на слайдере
        if (qteSlider != null)
            qteSlider.value = sliderValue;

        // Счётчик времени
        timer += Time.deltaTime;
        if (timer >= eventDuration)
        {
            // Время истекло, QTE не пройден
            QTEEnd(false);
        }

        // Проверка нажатия кнопки (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Проверяем, попали ли мы в зелёную зону
            if (sliderValue >= greenZoneMin && sliderValue <= greenZoneMax)
            {
                // Успех
                QTEEnd(true);
            }
            else
            {
                // Промах
                QTEEnd(false);
            }
        }
    }

    /// <summary>
    /// Запуск QTE (включаем панель, генерируем случайную зелёную зону, сбрасываем таймер и т.д.)
    /// </summary>
    public void StartQTE()
    {
        if (qtePanel != null) qtePanel.SetActive(true);

        timer = 0f;
        qteActive = true;
        sliderValue = 0f;
        direction = 1f;

        // Случайные границы зелёной зоны
        float zoneSize = UnityEngine.Random.Range(0.1f, 0.3f);             // Случайная ширина зоны (от 10% до 30%, например)
        greenZoneMin = UnityEngine.Random.Range(0f, 1f - zoneSize);        // Случайная позиция начала
        greenZoneMax = greenZoneMin + zoneSize;

        // Если хотим визуально подвигать зелёную зону
        if (greenZone != null && qteSlider != null)
        {
            // Берём ширину области FillArea у слайдера
            // Обычно полоса — это какая-то родительская зона для "Handle"
            float sliderWidth = (qteSlider.fillRect != null)
                                ? qteSlider.fillRect.rect.width
                                : qteSlider.GetComponent<RectTransform>().rect.width;

            // Вычисляем пиксельное смещение и ширину зелёной зоны
            float zonePixelMin = greenZoneMin * sliderWidth;
            float zonePixelWidth = zoneSize * sliderWidth;

            // Сдвигаем и ресайзим прямоугольник зелёной зоны
            greenZone.anchoredPosition = new Vector2(zonePixelMin, greenZone.anchoredPosition.y);
            greenZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, zonePixelWidth);
        }

        // Обнулить слайдер
        if (qteSlider != null)
        {
            qteSlider.value = 0f;
        }
    }

    /// <summary>
    /// Завершение QTE
    /// </summary>
    private void QTEEnd(bool success)
    {
        qteActive = false;
        if (qtePanel != null) qtePanel.SetActive(false);

        if (success)
        {
            Debug.Log("QTE Success!");
            OnSuccess?.Invoke();
        }
        else
        {
            Debug.Log("QTE Failed!");
            OnFail?.Invoke();
        }
    }
}
