using UnityEngine;

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

    void Start()
    {
        // Ищем все объекты на старте, которые принадлежат слоям dm1, dm2 и dmAlwaysActive
        objectsInDM1 = FindObjectsInLayer(layerDM1);
        objectsInDM2 = FindObjectsInLayer(layerDM2);
        objectsInDMAlwaysActive = FindObjectsInLayer(layerDMAlwaysActive);

        // На старте активируем только объекты в dm1 и dm1/2
        SetActiveForLayer(objectsInDM1, true);
        SetActiveForLayer(objectsInDM2, false);
        SetActiveForLayer(objectsInDMAlwaysActive, true);

        // Устанавливаем фоны
        background1.SetActive(true);
        background2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Проверка нажатия F
        {
            SwitchDimension();
        }
    }

    void SwitchDimension()
    {
        // Переключение фонов
        background1.SetActive(!background1.activeSelf);
        background2.SetActive(!background2.activeSelf);

        // Переключение объектов
        SetActiveForLayer(objectsInDM1, false);
        SetActiveForLayer(objectsInDM2, true);
    }

    // Функция для поиска всех объектов в слое
    GameObject[] FindObjectsInLayer(LayerMask layer)
    {
        return GameObject.FindObjectsOfType<GameObject>();
    }

    // Функция для установки активности объектов в определённом слое
    void SetActiveForLayer(GameObject[] objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (((1 << obj.layer) & layerDM1.value) != 0 || ((1 << obj.layer) & layerDM2.value) != 0 || ((1 << obj.layer) & layerDMAlwaysActive.value) != 0)
            {
                obj.SetActive(isActive);
            }
        }
    }
}
