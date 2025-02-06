using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestCounter : MonoBehaviour
{
    public static ChestCounter Instance;

    private Text chestCounterText; // UI Text элемент для отображения счетчика
    private int chestCount = 0;
    private GameObject canvasObj;

    private void Awake()
    {
        // Реализуем паттерн Singleton, чтобы этот объект был доступен из любого скрипта
        if (Instance == null)
        {
            Instance = this;
            // Открепляем объект от родителя, чтобы сделать его корневым
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject); // Не уничтожать объект при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Создаем Canvas и текстовый элемент, если они еще не созданы
        if (canvasObj == null)
        {
            CreateChestCounterText();
        }

        UpdateChestCounterText();
    }

    private void CreateChestCounterText()
    {
        // Создаем Canvas
        canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvasObj); // Не уничтожать Canvas при загрузке новой сцены

        // Создаем Text элемент
        GameObject textObj = new GameObject("ChestCounterText");
        textObj.transform.SetParent(canvasObj.transform, false);
        chestCounterText = textObj.AddComponent<Text>();

        // Настройка RectTransform
        RectTransform rectTransform = chestCounterText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.anchoredPosition = new Vector2(210, -50); // Сдвигаем на 150 пикселей вправо
        rectTransform.sizeDelta = new Vector2(200, 50);

        // Настройка текста
        chestCounterText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        chestCounterText.fontSize = 24;
        chestCounterText.color = Color.white; // Устанавливаем белый цвет текста
        chestCounterText.alignment = TextAnchor.MiddleLeft;
    }

    public void IncrementChestCount()
    {
        chestCount++;
        UpdateChestCounterText();
    }

    public void ResetChestCount()
    {
        chestCount = 0;
        UpdateChestCounterText();
    }

    private void UpdateChestCounterText()
    {
        // Проверяем, не был ли уничтожен текстовый элемент
        if (chestCounterText != null)
        {
            chestCounterText.text = "Chests: " + chestCount.ToString();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Если Canvas и текстовый элемент не найдены, создаем их
        if (canvasObj == null || chestCounterText == null)
        {
            CreateChestCounterText();
        }

        if (scene.name == "dead")
        {
            canvasObj.SetActive(false); // Скрываем Canvas на сцене dead
        }
        else
        {
            canvasObj.SetActive(true); // Показываем Canvas на всех остальных сценах
            UpdateChestCounterText(); // Обновляем текст счетчика
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события загрузки сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}