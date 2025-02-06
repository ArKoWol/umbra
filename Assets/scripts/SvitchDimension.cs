using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionShift : MonoBehaviour
{
    public LayerMask layerDM1; // Маска слоя dm1
    public LayerMask layerDM2; // Маска слоя dm2
    public LayerMask layerDMAlwaysActive; // Маска слоя dm1/2 (всегда активный слой)
    public GameObject background1; // Первый фон
    public GameObject background2; // Второй фон

    private GameObject[] objectsInDM1; // Объекты в dm1
    private GameObject[] objectsInDM2; // Объекты в dm2
    private GameObject[] objectsInDMAlwaysActive; // Объекты в слое dm1/2

    private bool isDM1Active = true; // Отслеживаем, какое измерение активно

    void Start()
    {
        // Проверяем, активна ли сцена, где должен работать скрипт
        if (SceneManager.GetActiveScene().name == "JumpScene")
        {
            Debug.LogWarning("DimensionShift отключен на сцене: JumpScene");
            enabled = false;
            return;
        }

        // Ищем все объекты на старте, которые принадлежат слоям dm1, dm2 и dmAlwaysActive
        objectsInDM1 = FindObjectsInLayer(layerDM1);
        objectsInDM2 = FindObjectsInLayer(layerDM2);
        objectsInDMAlwaysActive = FindObjectsInLayer(layerDMAlwaysActive);

        // На старте активируем только объекты в dm1 и dm1/2
        SetActiveForLayer(objectsInDM1, true);
        SetActiveForLayer(objectsInDM2, false);
        SetActiveForLayer(objectsInDMAlwaysActive, true);

        // Устанавливаем фоны
        if (background1 != null) background1.SetActive(true);
        if (background2 != null) background2.SetActive(false);
    }

    void Update()
    {
        if (!enabled) return;

        // Проверка нажатия клавиши E для переключения измерений
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchDimension();
        }
    }

    // Переключение между измерениями
    void SwitchDimension()
    {
        if (isDM1Active)
        {
            // Переключение на dm2
            SetActiveForLayer(objectsInDM1, false);
            SetActiveForLayer(objectsInDM2, true);
            if (background1 != null) background1.SetActive(false);
            if (background2 != null) background2.SetActive(true);
        }
        else
        {
            // Переключение на dm1
            SetActiveForLayer(objectsInDM1, true);
            SetActiveForLayer(objectsInDM2, false);
            if (background1 != null) background1.SetActive(true);
            if (background2 != null) background2.SetActive(false);
        }

        // Переключаем состояние
        isDM1Active = !isDM1Active;
        Debug.Log("Переключение измерений: " + (isDM1Active ? "DM1" : "DM2"));
    }

    // Поиск всех объектов в слое
    GameObject[] FindObjectsInLayer(LayerMask layer)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        return System.Array.FindAll(allObjects, obj => (layer.value & (1 << obj.layer)) != 0);
    }

    // Установка активности объектов в определённом слое
    void SetActiveForLayer(GameObject[] objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}